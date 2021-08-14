using Geo.Rest.ExceptionFilters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Rest.Controllers.Api
{
    [ReturnFriendlyExceptions]
#if (DEBUG == false)
    [ResponseCache(Duration = 86400, VaryByQueryKeys = new[] { "*" })]
#endif
    public abstract class BaseController : Controller
    {

    }
}
