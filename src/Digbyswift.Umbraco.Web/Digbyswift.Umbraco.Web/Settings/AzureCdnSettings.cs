using Microsoft.Extensions.Configuration;

namespace Digbyswift.Umbraco.Web.Settings;

public sealed class AzureCdnSettings
{
    public Uri BaseUrl { get; }
    public Uri MediaRootUrl { get; }
    public Uri AssetsRootUrl { get; }

    public AzureCdnSettings(IConfiguration config)
    {
        var cdnUrlRoot = config.GetValue<string>("Umbraco:Storage:Cdn:Url");
        if (String.IsNullOrWhiteSpace(cdnUrlRoot))
            throw new InvalidOperationException("Umbraco:Storage:Cdn:Url is not configured #startup");

        BaseUrl = new Uri(cdnUrlRoot, UriKind.Absolute);
        AssetsRootUrl = new Uri(BaseUrl, "assets");

        var containerName = config.GetValue<string>("Umbraco:Storage:AzureBlob:Media:ContainerName");
        if (String.IsNullOrWhiteSpace(cdnUrlRoot))
            throw new InvalidOperationException("Umbraco:Storage:AzureBlob:Media:ContainerName is not configured #startup");

        MediaRootUrl = new Uri(BaseUrl, containerName);
    }
}
