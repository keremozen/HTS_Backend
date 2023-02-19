using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HTS.Data;

/* This is used if database provider does't define
 * IHTSDbSchemaMigrator implementation.
 */
public class NullHTSDbSchemaMigrator : IHTSDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
