using Volo.Abp.Modularity;

namespace LmsAbp;

[DependsOn(
    typeof(LmsAbpDomainModule),
    typeof(LmsAbpTestBaseModule)
)]
public class LmsAbpDomainTestModule : AbpModule
{

}
