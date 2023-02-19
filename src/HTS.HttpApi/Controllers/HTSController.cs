using HTS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HTS.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HTSController : AbpControllerBase
{
    protected HTSController()
    {
        LocalizationResource = typeof(HTSResource);
    }
}
