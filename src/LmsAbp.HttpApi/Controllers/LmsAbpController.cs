using LmsAbp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace LmsAbp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LmsAbpController : AbpControllerBase
{
    protected LmsAbpController()
    {
        LocalizationResource = typeof(LmsAbpResource);
    }
}
