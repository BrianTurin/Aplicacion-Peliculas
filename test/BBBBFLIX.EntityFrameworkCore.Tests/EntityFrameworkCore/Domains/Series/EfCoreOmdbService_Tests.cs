using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBBBFLIX.EntityFrameworkCore;
using BBBBFLIX.Series;
using Xunit;
namespace BBBBFLIX.Series
{
    [Collection(BBBBFLIXTestConsts.CollectionDefinitionName)]
    public class EfCoreOmdbService_Tests : OmdbService_Tests<BBBBFLIXEntityFrameworkCoreTestModule>
    {
    }
}