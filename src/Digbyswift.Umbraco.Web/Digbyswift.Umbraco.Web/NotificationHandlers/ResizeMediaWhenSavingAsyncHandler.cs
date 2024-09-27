using Digbyswift.Core.Extensions;
using Digbyswift.Umbraco.Web.Providers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Extensions;
using uConstants = Umbraco.Cms.Core.Constants;

namespace Digbyswift.Umbraco.Web.NotificationHandlers;

/// <summary>
/// Resizes images upon saving, to a fixed width and/or height of 2200px. Requires registration of an IFileSystemProvider.
/// </summary>
public sealed class ResizeMediaWhenSavingAsyncHandler : INotificationAsyncHandler<MediaSavingNotification>
{
    private static readonly string[] SupportedTypes = { ".jpeg", ".jpg", ".gif", ".bmp", ".png", ".tiff", ".tif" };
    private const int MaxImageWidth = 2200;
    private const int MaxImageHeight = 2200;

    private readonly IFileSystemProvider _fileSystemProvider;
    private readonly ILogger _logger;

    public ResizeMediaWhenSavingAsyncHandler(IFileSystemProvider fileSystemProvider, ILogger<ResizeMediaWhenSavingAsyncHandler> logger)
    {
        _fileSystemProvider = fileSystemProvider;
        _logger = logger;
    }

    public async Task HandleAsync(MediaSavingNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var media in notification.SavedEntities)
            {
                if (!TryGetImagePath(media, out var relativeImagePath) || relativeImagePath == null)
                    continue;

                await using (var stream = await _fileSystemProvider.GetAsStreamAsync(relativeImagePath))
                using (var image = Image.Load(stream, out var format))
                {
                    var originalWidth = image.Width;
                    var originalHeight = image.Height;

                    if (originalWidth < MaxImageWidth && originalHeight < MaxImageHeight)
                        return;

                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(MaxImageWidth, MaxImageHeight),
                        Sampler = KnownResamplers.Lanczos3,
                        Mode = ResizeMode.Max
                    }));

                    using (var tempStream = new MemoryStream())
                    {
                        await image.SaveAsync(tempStream, format, cancellationToken);

                        // Reset stream
                        tempStream.Position = 0;

                        // Upload
                        await _fileSystemProvider.SaveAsync(tempStream, relativeImagePath, format.DefaultMimeType, disposeOfStream: false);

                        media.SetValue(uConstants.Conventions.Media.Width, image.Width);
                        media.SetValue(uConstants.Conventions.Media.Height, image.Height);
                        media.SetValue(uConstants.Conventions.Media.Bytes, tempStream.Length);

                        _logger.LogInformation("Media {relativeImagePath} ({originalWidth} x {originalHeight}) resized on upload #media", relativeImagePath, originalWidth, originalHeight);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Media resize on upload failed #media");
        }
    }
        
    private static bool TryGetImagePath(IContentBase media, out string? imagePath)
    {
        imagePath = null;
            
        if (!media.HasProperty(uConstants.Conventions.Media.File))
            return false;
                    
        var umbracoFileProp = media.GetValue<string>(uConstants.Conventions.Media.File);
        if (String.IsNullOrWhiteSpace(umbracoFileProp))
            return false;

        string? workingImagePath;
        if (umbracoFileProp.DetectIsJson())
        {
            var cropObject = JsonConvert.DeserializeObject<ImageCropperValue>(umbracoFileProp);
            if (cropObject == null)
                return false;

            workingImagePath = cropObject.Src;
        }
        else
        {
            workingImagePath = umbracoFileProp;
        }

        if (String.IsNullOrWhiteSpace(workingImagePath))
            return false;

        string extension = Path.GetExtension(workingImagePath);
        if (!SupportedTypes.ContainsIgnoreCase(extension))
            return false;

        imagePath = workingImagePath;
        return true;
    }
        
}