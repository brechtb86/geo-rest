using Geo.Rest.ExceptionFilters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Rest.Controllers.Api
{
    [ReturnFriendlyExceptions]
    public abstract class BaseController : Controller
    {

    }
}
