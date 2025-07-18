using BBBBFLIX.Samples;
using Xunit;

namespace BBBBFLIX.EntityFrameworkCore.Domains;

[Collection(BBBBFLIXTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<BBBBFLIXEntityFrameworkCoreTestModule>
{

}
