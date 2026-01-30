using Volo.Abp.Modularity;

namespace LmsAbp;

/* Inherit from this class for your domain layer tests. */
public abstract class LmsAbpDomainTestBase<TStartupModule> : LmsAbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
