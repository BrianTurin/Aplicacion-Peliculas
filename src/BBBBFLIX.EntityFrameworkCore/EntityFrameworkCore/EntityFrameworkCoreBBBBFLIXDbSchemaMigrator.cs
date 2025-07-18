using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BBBBFLIX.Data;
using Volo.Abp.DependencyInjection;

namespace BBBBFLIX.EntityFrameworkCore;

public class EntityFrameworkCoreBBBBFLIXDbSchemaMigrator
    : IBBBBFLIXDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBBBBFLIXDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BBBBFLIXDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BBBBFLIXDbContext>()
            .Database
            .MigrateAsync();
    }
}
