﻿using Geo.Rest.Business.Services.Interfaces;
using Geo.Rest.Domain.Models.Geo;
using Geo.Rest.Domain.Models.Query;
using Geo.Rest.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Geo.Rest.Business.Extensions;

namespace Geo.Rest.Controllers.Api
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
        public async Task<IActionResult> GetCountriesAsync([FromQuery] CountryCollectionQueryParameters parameters)
        {
            var countries = await _geoService.GetCountriesAsync(parameters);

            var dynamicCountries = countries.SelectFields(parameters.Fields);


            return this.Ok(dynamicCountries);
        }

        [HttpGet("countries/{countryId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Country), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCountryByIdAsync(int countryId, [FromQuery] CountryItemQueryParameters parameters)
        {
            var country = await _geoService.GetCountryByIdAsync(countryId, parameters);

            var dynamicCountry = country.SelectFields(parameters.Fields);

            return this.Ok(dynamicCountry);
        }

        [HttpGet("countries/{twoLetterIsoCode}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Country), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCountryByTwoLetterIsoCodeAsync(string twoLetterIsoCode, [FromQuery] CountryItemQueryParameters parameters)
        {
            var country = await _geoService.GetCountryByTwoLetterIsoCodeAsync(twoLetterIsoCode, parameters);

            var dynamicCountry = country.SelectFields(parameters.Fields);

            return this.Ok(dynamicCountry);
        }

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