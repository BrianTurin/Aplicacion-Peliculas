using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BBBBFLIX.Data;

/* This is used if database provider does't define
 * IBBBBFLIXDbSchemaMigrator implementation.
 */
public class NullBBBBFLIXDbSchemaMigrator : IBBBBFLIXDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
