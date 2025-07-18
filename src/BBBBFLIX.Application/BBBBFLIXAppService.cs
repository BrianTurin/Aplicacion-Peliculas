using BBBBFLIX.Localization;
using Volo.Abp.Application.Services;

namespace BBBBFLIX;

/* Inherit your application services from this class.
 */
public abstract class BBBBFLIXAppService : ApplicationService
{
    protected BBBBFLIXAppService()
    {
        LocalizationResource = typeof(BBBBFLIXResource);
    }
}
