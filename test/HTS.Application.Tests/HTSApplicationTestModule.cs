using Volo.Abp.Modularity;

namespace HTS;

[DependsOn(
    typeof(HTSApplicationModule),
    typeof(HTSDomainTestModule)
    )]
public class HTSApplicationTestModule : AbpModule
{

}
