using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Rest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.RedirectPermanent("https://github.com/brechtb86/geo-rest/blob/main/README.md");
        }
    }
}
