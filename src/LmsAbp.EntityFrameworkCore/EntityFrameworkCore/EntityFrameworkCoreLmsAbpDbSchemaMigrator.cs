using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LmsAbp.Data;
using Volo.Abp.DependencyInjection;

namespace LmsAbp.EntityFrameworkCore;

public class EntityFrameworkCoreLmsAbpDbSchemaMigrator
    : ILmsAbpDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreLmsAbpDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the LmsAbpDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<LmsAbpDbContext>()
            .Database
            .MigrateAsync();
    }
}
