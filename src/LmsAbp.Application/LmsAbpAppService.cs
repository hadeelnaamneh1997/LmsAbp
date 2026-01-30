using System;
using System.Collections.Generic;
using System.Text;
using LmsAbp.Localization;
using Volo.Abp.Application.Services;

namespace LmsAbp;

/* Inherit your application services from this class.
 */
public abstract class LmsAbpAppService : ApplicationService
{
    protected LmsAbpAppService()
    {
        LocalizationResource = typeof(LmsAbpResource);
    }
}
