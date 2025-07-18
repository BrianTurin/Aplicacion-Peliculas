using System.Threading.Tasks;

namespace BBBBFLIX.Data;

public interface IBBBBFLIXDbSchemaMigrator
{
    Task MigrateAsync();
}
