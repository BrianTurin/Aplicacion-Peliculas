using Volo.Abp.Modularity;

namespace BBBBFLIX;

/* Inherit from this class for your domain layer tests. */
public abstract class BBBBFLIXDomainTestBase<TStartupModule> : BBBBFLIXTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
