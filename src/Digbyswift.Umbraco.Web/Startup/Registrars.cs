#pragma warning disable SA1402
#pragma warning disable SA1649
using Umbraco.Cms.Core.Sync;

namespace Digbyswift.Umbraco.Web.Startup;

public sealed class SingleRoleRegistrar : IServerRoleAccessor
{
    public ServerRole CurrentServerRole => ServerRole.Single;
}

public sealed class SubscriberRoleRegistrar : IServerRoleAccessor
{
    public ServerRole CurrentServerRole => ServerRole.Subscriber;
}

public sealed class SchedulingPublisherRoleRegistrar : IServerRoleAccessor
{
    public ServerRole CurrentServerRole => ServerRole.SchedulingPublisher;
}
