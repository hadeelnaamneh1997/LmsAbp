using Volo.Abp.Modularity;

namespace LmsAbp;

[DependsOn(
    typeof(LmsAbpApplicationModule),
    typeof(LmsAbpDomainTestModule)
)]
public class LmsAbpApplicationTestModule : AbpModule
{

}
