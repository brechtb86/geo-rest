using Geo.Rest.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Rest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGeoService _geoService;

        public HomeController(IGeoService geoService)
        {
            this._geoService = geoService;
        }

        public IActionResult Index()
        {
            return this.RedirectPermanent("https://github.com/brechtb86/geo-rest/blob/main/README.md");
        }

        public IActionResult Export([FromQuery] string databaseName = "Geo")
        {
            var exportScript = this._geoService.GenerateExportScript(databaseName);

            return this.PhysicalFile(exportScript, "application/sql", Path.GetFileName(exportScript));
        }
    }
}
