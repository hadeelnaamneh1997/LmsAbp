using LmsAbp.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace LmsAbp.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class LmsAbpPageModel : AbpPageModel
{
    protected LmsAbpPageModel()
    {
        LocalizationResourceType = typeof(LmsAbpResource);
    }
}
