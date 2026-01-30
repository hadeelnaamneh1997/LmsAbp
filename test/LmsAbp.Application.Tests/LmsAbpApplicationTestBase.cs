using Volo.Abp.Modularity;

namespace LmsAbp;

public abstract class LmsAbpApplicationTestBase<TStartupModule> : LmsAbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
