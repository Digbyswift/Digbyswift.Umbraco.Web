using Microsoft.Extensions.Configuration;

namespace Digbyswift.Umbraco.Web.Settings;

public sealed class ConnectionStringSettings
{
    public const string SectionName = "ConnectionStrings";

    [ConfigurationKeyName("umbracoDbDSN")]
    public string? Umbraco { get; set; }

    [ConfigurationKeyName("AzureStorageConnection")]
    public string? AzureStorage { get; set; }
}
