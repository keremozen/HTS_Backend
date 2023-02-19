using HTS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace HTS.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HTSEntityFrameworkCoreModule),
    typeof(HTSApplicationContractsModule)
    )]
public class HTSDbMigratorModule : AbpModule
{

}
