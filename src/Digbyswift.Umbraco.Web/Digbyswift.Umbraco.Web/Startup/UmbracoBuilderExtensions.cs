using Digbyswift.Umbraco.Web.ImageSharp;
using Digbyswift.Umbraco.Web.Settings;
using Examine;
using Examine.Lucene;
using Examine.Lucene.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp.Web.Middleware;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Infrastructure.Examine.DependencyInjection;
using Umbraco.Cms.Infrastructure.PublishedCache;
using Umbraco.Cms.Web.Website.Controllers;
using UmbConstants = Umbraco.Cms.Core.Constants;

namespace Digbyswift.Umbraco.Web.Startup;

public static class UmbracoBuilderExtensions
{
    public static IUmbracoBuilder AddConnectionStrings(this IUmbracoBuilder builder)
    {
        builder.Services.Configure<ConnectionStringSettings>(builder.Config.GetSection(ConnectionStringSettings.SectionName));

        return builder;
    }

    public static IUmbracoBuilder AddEnvironmentSettings(this IUmbracoBuilder builder)
    {
        builder.Services.AddSingleton(new EnvironmentSettings(builder.Config));

        return builder;
    }

    public static IUmbracoBuilder AddAzureCdnSettings(this IUmbracoBuilder builder)
    {
        builder.Services.AddSingleton(new AzureCdnSettings(builder.Config));

        return builder;
    }

    public static IUmbracoBuilder AddEmailSettings(this IUmbracoBuilder builder)
    {
        builder.Services.AddSingleton(new EmailSettings(builder.Config));

        return builder;
    }

    public static IUmbracoBuilder AddDefaultController<T>(this IUmbracoBuilder builder) where T : Controller
    {
        builder.Services.Configure<UmbracoRenderingDefaultsOptions>(c => { c.DefaultControllerType = typeof(T); });

        return builder;
    }

    public static IUmbracoBuilder AddImageSharpCommandParsing(this IUmbracoBuilder builder)
    {
        var maxWidth = builder.Config.GetValue<int?>("Umbraco:CMS:Imaging:Resize:MaxWidth") ?? ImageSharpConstants.DefaultMaxWidth;
        var maxHeight = builder.Config.GetValue<int?>("Umbraco:CMS:Imaging:Resize:MaxHeight") ?? ImageSharpConstants.DefaultMaxHeight;
        var minQuality = builder.Config.GetValue<int?>("Umbraco:CMS:Imaging:Resize:MinQuality") ?? ImageSharpConstants.DefaultMinQuality;

        builder.Services.Configure<ImageSharpMiddlewareOptions>(options =>
        {
            options.OnParseCommandsAsync = context => ImageSharpOptionsHelper.ParseCommandsAsync(context, maxWidth, maxHeight, minQuality);
        });

        return builder;
    }

    public static IUmbracoBuilder IgnoreLocalDb(this IUmbracoBuilder builder)
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
        var descriptor = new UniqueServiceDescriptor(
            typeof(INotificationHandler<TNotification>),
            typeof(TNotificationHandler),
            ServiceLifetime.Transient);

        if (builder.Services.Contains(descriptor))
        {
            builder.Services.Remove(descriptor);
        }

        return builder;
    }

    /// <summary>
    /// <para>Removes Examine index registrations. These are set in
    /// <see cref="global::Umbraco.Cms.Infrastructure.Examine.DependencyInjection.UmbracoBuilderExtensions.AddExamineIndexes">
    /// Umbraco.Cms.Infrastructure.Examine.DependencyInjection.UmbracoBuilderExtensions.AddExamineIndexes()</see>.</para>
    ///
    /// <para>If removing an index, ensure that you also remove access to it in the CMS. For example, disabling
    /// the member index will cause an error when using the CMS global search unless the Member section is removed
    /// from the user's access.</para>
    /// </summary>
    public static IUmbracoBuilder DisableExamineIndexes(this IUmbracoBuilder builder, string[] indexNamesToDisable)
    {
        if (indexNamesToDisable.Length == 0)
            return builder;

        // Remove all Examine indexes
        builder.Services.RemoveAll<IIndex>();

        // Remove all Examine index configuration options
        builder.Services.RemoveAll<IConfigureOptions<LuceneDirectoryIndexOptions>>();

        AddIndexIfNotDisabled<UmbracoContentIndex>(UmbConstants.UmbracoIndexes.InternalIndexName);
        AddIndexIfNotDisabled<UmbracoContentIndex>(UmbConstants.UmbracoIndexes.ExternalIndexName);
        AddIndexIfNotDisabled<UmbracoMemberIndex>(UmbConstants.UmbracoIndexes.MembersIndexName);
        AddIndexIfNotDisabled<DeliveryApiContentIndex>(UmbConstants.UmbracoIndexes.DeliveryApiContentIndexName);

        // Reconfigure Examine index options
        builder.Services.ConfigureOptions<ConfigureIndexOptions>();

        return builder;

        void AddIndexIfNotDisabled<TIndex>(string indexName)
            where TIndex : LuceneIndex
        {
            if (!indexNamesToDisable.Contains(indexName))
            {
                builder.Services.AddExamineLuceneIndex<TIndex, ConfigurationEnabledDirectoryFactory>(indexName);
            }
        }
    }
}
