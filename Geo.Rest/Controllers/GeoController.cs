using Geo.Business.Services.Interfaces;
using Geo.Domain.Models.Geo;
using Geo.Domain.Models.Query;
using Geo.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Rest.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/")]
    public class GeoController : BaseController
    {
        private readonly IGeoService _geoService;

        public GeoController(IGeoService geoService)
        {
            this._geoService = geoService;
        }

        [HttpGet("countries")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WrappedCollection<Country>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCountriesAsync([FromQuery] CollectionQueryParameters parameters)
        {
            var countries = await this._geoService.GetCountriesAsync(parameters);

            return this.Ok(countries);
        }

        //[HttpGet("countries/{id:int}")]
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(Country), 200)]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> GetCountryByIdAsync(int id)
        //{
        //    var country = await this._geoService.GetCountryByIdAsync(id);

        //    return this.Ok(country);
        //}

        //[HttpGet("states")]
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(PagedCollection<State>), 200)]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> GetStatesAsync([FromQuery] CollectionQueryParameters parameters)
        //{
        //    var states = await this._geoService.GetStatesAsync(parameters);

        //    return this.Ok(states);
        //}

        //[HttpGet("states/{id:int}")]
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(State), 200)]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> GetStateByIdAsync(int id)
        //{
        //    var state = await this._geoService.GetStateByIdAsync(id);

        //    return this.Ok(state);
        //}

        //[HttpGet("countries/{countryId:int}/states")]
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(ICollection<State>), 200)]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> GetStatesByCountryIdAsync(int countryId)
        //{
        //    var states = await this._geoService.GetStatesByCountryIdAsync(countryId);

        //    return this.Ok(states);
        //}
    }
}
