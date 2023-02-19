using HTS.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HTS;

[DependsOn(
    typeof(HTSEntityFrameworkCoreTestModule)
    )]
public class HTSDomainTestModule : AbpModule
{

}
