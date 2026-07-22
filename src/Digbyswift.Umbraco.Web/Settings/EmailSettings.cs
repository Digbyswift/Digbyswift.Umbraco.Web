using Microsoft.Extensions.Configuration;

namespace Digbyswift.Umbraco.Web.Settings;

public class EmailSettings : Digbyswift.AspNet.Settings.EmailSettings
{
    public override SmtpOptions SmtpSection { get; }

    public EmailSettings(IConfiguration config) : base(config)
    {
        SmtpSection = config.GetSection(SmtpOptions.SectionName).Get<SmtpOptions>() ?? new SmtpOptions();
    }
}
