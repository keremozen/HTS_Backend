using System.Threading.Tasks;

namespace HTS.Data;

public interface IHTSDbSchemaMigrator
{
    Task MigrateAsync();
}
