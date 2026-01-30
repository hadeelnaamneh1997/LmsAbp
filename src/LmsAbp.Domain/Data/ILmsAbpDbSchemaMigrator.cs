using System.Threading.Tasks;

namespace LmsAbp.Data;

public interface ILmsAbpDbSchemaMigrator
{
    Task MigrateAsync();
}
