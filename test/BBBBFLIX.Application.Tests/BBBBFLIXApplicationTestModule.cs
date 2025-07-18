using Volo.Abp.Modularity;

namespace BBBBFLIX;

[DependsOn(
   typeof(BBBBFLIXApplicationModule),
   typeof(BBBBFLIXDomainTestModule)
)]
public class BBBBFLIXApplicationTestModule : AbpModule
{

}
