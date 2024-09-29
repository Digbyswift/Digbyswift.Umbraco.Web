using Digbyswift.Core.Extensions;
using Digbyswift.Umbraco.Web.ImageSharp;
using Digbyswift.Umbraco.Web.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SixLabors.ImageSharp.Formats.Png;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Extensions;
using uConstants = Umbraco.Cms.Core.Constants;

namespace Digbyswift.Umbraco.Web.NotificationHandlers;

/// <summary>
/// Resizes images upon saving, to a fixed width and/or height of whatever is set in the config
/// for the following keys or 3000px by default. Requires registration of an IFileSystemProvider.
/// <list type="bullet">
/// <item>Umbraco:CMS:Imaging:Resize:MaxWidth</item>
/// <item>Umbraco:CMS:Imaging:Resize:MaxHeight</item>
/// </list>
/// </summary>
public sealed class ResizeMediaWhenSavingAsyncHandler : INotificationAsyncHandler<MediaSavingNotification>
{
    private readonly IFileSystemProvider _fileSystemProvider;
    private readonly ILogger _logger;
    private readonly int _maxWidth;
    private readonly int _maxHeight;

    public ResizeMediaWhenSavingAsyncHandler(
        IFileSystemProvider fileSystemProvider,
        IConfiguration configuration,
        ILogger<ResizeMediaWhenSavingAsyncHandler> logger)
    {
        _fileSystemProvider = fileSystemProvider;
        _logger = logger;

        _maxWidth = configuration.GetValue<int?>("Umbraco:CMS:Imaging:Resize:MaxWidth") ?? ImageSharpConstants.DefaultMaxWidth;
        _maxHeight = configuration.GetValue<int?>("Umbraco:CMS:Imaging:Resize:MaxHeight") ?? ImageSharpConstants.DefaultMaxHeight;

        _logger.LogInformation("Resizing handler registered with values: {maxWidth} x {maxHeight} #media", _maxWidth, _maxHeight);
    }

    public async Task HandleAsync(MediaSavingNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var media in notification.SavedEntities)
            {
                if (!TryGetImagePath(media, out var relativeImagePath) || relativeImagePath == null)
                    continue;

                await using var stream = await _fileSystemProvider.GetAsStreamAsync(relativeImagePath);
                using var image = await Image.LoadAsync(stream, cancellationToken);

                var originalFormat = image.Metadata.DecodedImageFormat ?? PngFormat.Instance;
                var originalWidth = image.Width;
                var originalHeight = image.Height;

                if (originalWidth < _maxWidth && originalHeight < _maxHeight)
                    return;

                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(_maxWidth, _maxHeight),
                    Sampler = KnownResamplers.Lanczos3,
                    Mode = ResizeMode.Max
                }));

                using (var tempStream = new MemoryStream())
                {
                    await image.SaveAsync(tempStream, originalFormat, cancellationToken);

                    // Reset stream
                    tempStream.Position = 0;

                    // Upload
                    await _fileSystemProvider.SaveAsync(tempStream, relativeImagePath, originalFormat.DefaultMimeType, disposeOfStream: false);

                    media.SetValue(uConstants.Conventions.Media.Width, image.Width);
                    media.SetValue(uConstants.Conventions.Media.Height, image.Height);
                    media.SetValue(uConstants.Conventions.Media.Bytes, tempStream.Length);

                    _logger.LogInformation("Media {relativeImagePath} ({originalWidth} x {originalHeight}) resized on upload #media", relativeImagePath, originalWidth, originalHeight);
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

        var extension = Path.GetExtension(workingImagePath);
        if (!ImageSharpConstants.GetSupportedExtensions.ContainsIgnoreCase(extension))
            return false;

        imagePath = workingImagePath;
        return true;
    }
}
