using Digbyswift.Umbraco.Web.ImageSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.Web.Middleware;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Infrastructure.DependencyInjection;
using Umbraco.Cms.Infrastructure.PublishedCache;
using Umbraco.Cms.Web.Website.Controllers;

namespace Digbyswift.Umbraco.Web.Startup;

public static class UmbracoBuilderExtensions
{
    public static IUmbracoBuilder AddRegistrar(this IUmbracoBuilder builder)
    {
        var logger = builder.BuilderLoggerFactory.CreateLogger(nameof(UmbracoBuilderExtensions));

        if (!envSettings.HasSeparateCmsInstance)
        {
            builder.SetServerRegistrar<SingleRoleRegistrar>();
            logger.LogInformation("Registrar: {registrar} #startup", nameof(SingleRoleRegistrar));
            return builder;
        }

        if (!envSettings.IsCms())
        {
            builder.SetServerRegistrar<SubscriberRoleRegistrar>();
            logger.LogInformation("Registrar: {registrar} #startup", nameof(SubscriberRoleRegistrar));
        }
        else if (envSettings.IsCms() && !envSettings.IsDeployment())
        {
            builder.SetServerRegistrar<SchedulingPublisherRoleRegistrar>();
            logger.LogInformation("Registrar: {registrar} #startup", nameof(SchedulingPublisherRoleRegistrar));
        }

        return builder;
    }

    public static IUmbracoBuilder AddDefaultController<T>(this IUmbracoBuilder builder) where T : Controller
    {
        builder.Services.Configure<UmbracoRenderingDefaultsOptions>(c => { c.DefaultControllerType = typeof(T); });

        return builder;
    }

    public static IUmbracoBuilder AddImageSharpCommandParsing(this IUmbracoBuilder builder)
    {
        builder.Services.Configure<ImageSharpMiddlewareOptions>(options => { options.OnParseCommandsAsync = ImageSharpOptionsHelper.ParseCommandsAsync; });

        return builder;
    }

    public static IUmbracoBuilder SetLocalDb(this IUmbracoBuilder builder)
    {
        if (builder.Config.GetValue("Umbraco:CMS:NuCache:IgnoreLocalDb", false))
        {
            builder.Services.AddSingleton(new PublishedSnapshotServiceOptions
            {
                IgnoreLocalDb = true
            });
        }

        return builder;
    }

    public static IUmbracoBuilder RemoveNotificationHandler<TNotification, TNotificationHandler>(
        this IUmbracoBuilder builder)
        where TNotificationHandler : INotificationHandler<TNotification>
        where TNotification : INotification
    {
        builder.Services.RemoveNotificationHandler<TNotification, TNotificationHandler>();
        return builder;
    }

    private static IServiceCollection RemoveNotificationHandler<TNotification, TNotificationHandler>(
        this IServiceCollection services)
        where TNotificationHandler : INotificationHandler<TNotification>
        where TNotification : INotification
    {
        var descriptor = new UniqueServiceDescriptor(
            typeof(INotificationHandler<TNotification>),
            typeof(TNotificationHandler),
            ServiceLifetime.Transient);

        if (services.Contains(descriptor))
        {
            services.Remove(descriptor);
        }

        return services;
    }
}
