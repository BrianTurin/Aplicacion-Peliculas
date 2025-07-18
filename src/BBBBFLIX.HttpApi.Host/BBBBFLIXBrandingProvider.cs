using Microsoft.Extensions.Localization;
using BBBBFLIX.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace BBBBFLIX;

[Dependency(ReplaceServices = true)]
public class BBBBFLIXBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<BBBBFLIXResource> _localizer;

    public BBBBFLIXBrandingProvider(IStringLocalizer<BBBBFLIXResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
