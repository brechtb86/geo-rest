﻿using Geo.Rest.Business.Services.Interfaces;
using Geo.Rest.Domain.Models.Geo;
using Geo.Rest.Domain.Models.Query;
using Geo.Rest.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Geo.Rest.Business.Extensions;
using System.Threading;

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
        public async Task<IActionResult> GetCountriesAsync([FromQuery] CollectionQueryParameters parameters)
        {
            var countries = await _geoService.GetCountriesAsync(parameters);

            var dynamicCountries = countries.SelectFields(parameters.FieldsList);

            return this.Ok(dynamicCountries);
        }

        [HttpGet("countries/{countryId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Country), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCountryByIdAsync(int countryId, [FromQuery] QueryParameters parameters)
        {
            var country = await _geoService.GetCountryByIdAsync(countryId, parameters);

            if(country == null)
            {
                return this.NotFound($"The requested endpoint is correct but a country with id '{countryId}' cannot be found");
            }

            var dynamicCountry = country.SelectFields(parameters.FieldsList);

            return this.Ok(dynamicCountry);
        }

        [HttpGet("countries/{twoLetterIsoCode}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Country), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCountryByTwoLetterIsoCodeAsync(string twoLetterIsoCode, [FromQuery] QueryParameters parameters)
        {
            var country = await _geoService.GetCountryByTwoLetterIsoCodeAsync(twoLetterIsoCode, parameters);

            if (country == null)
            {
                return this.NotFound($"The requested endpoint is correct but a country with two letter isocode '{twoLetterIsoCode}' cannot be found");
            }

            var dynamicCountry = country.SelectFields(parameters.FieldsList);

            return this.Ok(dynamicCountry);
        }

        [HttpGet("countries/{countryId:int}/states")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WrappedCollection<State>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetStatesByCountryAsync(int countryId, [FromQuery] CollectionQueryParameters parameters)
        {
            var states = await _geoService.GetStatesByCountryAsync(countryId, parameters);
            
            var dynamiStates = states.SelectFields(parameters.FieldsList);

            return this.Ok(dynamiStates);
        }

        [HttpGet("countries/{countryCode}/states")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WrappedCollection<State>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetStatesByCountryAsync(string countryCode, [FromQuery] CollectionQueryParameters parameters)
        {
            var states = await _geoService.GetStatesByCountryAsync(countryCode, parameters);

            var dynamiStates = states.SelectFields(parameters.FieldsList);

            return this.Ok(dynamiStates);
        }

        [HttpGet("countries/{countryId:int}/cities")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WrappedCollection<City>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCitiesByCountryAsync(int countryId, [FromQuery] CollectionQueryParameters parameters)
        {
            var cities = await _geoService.GetCitiesByCountryAsync(countryId, parameters);

            var dynamicCities = cities.SelectFields(parameters.FieldsList);

            return this.Ok(dynamicCities);
        }

        [HttpGet("countries/{countryCode}/cities")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WrappedCollection<City>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCitiesByCountryAsync(string countryCode, [FromQuery] CollectionQueryParameters parameters)
        {
            var cities = await _geoService.GetCitiesByCountryAsync(countryCode, parameters);

            var dynamicCities = cities.SelectFields(parameters.FieldsList);

            return this.Ok(dynamicCities);
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
