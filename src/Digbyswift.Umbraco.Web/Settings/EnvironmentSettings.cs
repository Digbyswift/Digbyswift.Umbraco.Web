using Digbyswift.Core.Constants;
using Microsoft.Extensions.Configuration;

namespace Digbyswift.Umbraco.Web.Settings;

public class EnvironmentSettings : Digbyswift.AspNet.Settings.EnvironmentSettings
{
    private readonly string? _siteBaseUrl;
    private readonly string? _backofficeUrl;
    private bool? _hasSeparateCmsInstance;

    public bool HasSeparateCmsInstance
    {
        get
        {
            if (_hasSeparateCmsInstance.HasValue)
                return _hasSeparateCmsInstance.Value;

            if (String.IsNullOrWhiteSpace(_backofficeUrl) ||
                String.IsNullOrWhiteSpace(_siteBaseUrl) ||
                _backofficeUrl.StartsWith(CharConstants.ForwardSlash) ||
                !_backofficeUrl.StartsWith(_siteBaseUrl))
            {
                _hasSeparateCmsInstance = false;
            }
            else
            {
                _hasSeparateCmsInstance = true;
            }

            return _hasSeparateCmsInstance.Value;
        }
    }

    public EnvironmentSettings(IConfiguration config) : base(config)
    {
        _siteBaseUrl = config.GetValue<string>("Site:BaseUrl");
        _backofficeUrl = config.GetValue<string>("Umbraco:CMS:WebRouting:UmbracoApplicationUrl");
    }
}
