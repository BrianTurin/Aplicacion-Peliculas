using BBBBFLIX.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace BBBBFLIX.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BBBBFLIXEntityFrameworkCoreModule),
    typeof(BBBBFLIXApplicationContractsModule)
)]
public class BBBBFLIXDbMigratorModule : AbpModule
{
}
