using System;
using System.Collections.Generic;
using System.Text;
using HTS.Localization;
using Volo.Abp.Application.Services;

namespace HTS;

/* Inherit your application services from this class.
 */
public abstract class HTSAppService : ApplicationService
{
    protected HTSAppService()
    {
        LocalizationResource = typeof(HTSResource);
    }
}
