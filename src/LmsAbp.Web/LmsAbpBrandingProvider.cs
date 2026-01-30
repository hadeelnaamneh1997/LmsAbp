using Microsoft.Extensions.Localization;
using LmsAbp.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace LmsAbp.Web;

[Dependency(ReplaceServices = true)]
public class LmsAbpBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<LmsAbpResource> _localizer;

    public LmsAbpBrandingProvider(IStringLocalizer<LmsAbpResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
