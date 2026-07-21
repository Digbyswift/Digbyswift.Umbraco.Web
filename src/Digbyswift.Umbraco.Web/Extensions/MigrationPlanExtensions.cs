using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class MigrationPlanExtensions
{
    public static async Task<ExecutedMigrationPlan> ExecuteAsync(this MigrationPlan plan,
        IMigrationPlanExecutor migrationPlanExecutor,
        ICoreScopeProvider scopeProvider,
        IKeyValueService keyValueService)
    {
        var upgrader = new Upgrader(plan);

        return await upgrader.ExecuteAsync(migrationPlanExecutor, scopeProvider, keyValueService);
    }
}
