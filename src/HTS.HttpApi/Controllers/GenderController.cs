using HTS.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace HTS.Controllers
{
    public class GenderController : AbpControllerBase
    {
        public GenderController() {
            LocalizationResource = typeof(HTSResource);
        }


    }
}
