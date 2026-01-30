using LmsAbp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace LmsAbp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LmsAbpEntityFrameworkCoreModule),
    typeof(LmsAbpApplicationContractsModule)
    )]
public class LmsAbpDbMigratorModule : AbpModule
{
}
