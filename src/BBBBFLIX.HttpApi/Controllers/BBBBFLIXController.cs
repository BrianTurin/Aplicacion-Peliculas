using BBBBFLIX.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BBBBFLIX.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BBBBFLIXController : AbpControllerBase
{
    protected BBBBFLIXController()
    {
        LocalizationResource = typeof(BBBBFLIXResource);
    }
}
