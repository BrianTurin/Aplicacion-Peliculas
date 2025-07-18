using Volo.Abp.Modularity;

namespace BBBBFLIX;

[DependsOn(
    typeof(BBBBFLIXDomainModule),
    typeof(BBBBFLIXTestBaseModule)
)]
public class BBBBFLIXDomainTestModule : AbpModule
{

}
