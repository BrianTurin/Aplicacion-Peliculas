using Volo.Abp.Modularity;

namespace BBBBFLIX;

public abstract class BBBBFLIXApplicationTestBase<TStartupModule> : BBBBFLIXTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
