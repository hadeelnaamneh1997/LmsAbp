using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LmsAbp.Data;

/* This is used if database provider does't define
 * ILmsAbpDbSchemaMigrator implementation.
 */
public class NullLmsAbpDbSchemaMigrator : ILmsAbpDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
