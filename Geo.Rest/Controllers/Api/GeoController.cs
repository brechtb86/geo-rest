using Geo.Rest.Business.Services.Interfaces;
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

        [HttpGet("countries/{countryCode}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Country), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCountryByTwoLetterIsoCodeAsync(string countryCode, [FromQuery] QueryParameters parameters)
        {
            var country = await _geoService.GetCountryByTwoLetterIsoCodeAsync(countryCode, parameters);

            if (country == null)
            {
                return this.NotFound($"The requested endpoint is correct but a country with two letter iso code '{countryCode}' cannot be found");
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
            
            var dynamicStates = states.SelectFields(parameters.FieldsList);

            return this.Ok(dynamicStates);
        }

        [HttpGet("countries/{countryCode}/states")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WrappedCollection<State>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetStatesByCountryAsync(string countryCode, [FromQuery] CollectionQueryParameters parameters)
        {
            var states = await _geoService.GetStatesByCountryAsync(countryCode, parameters);

            var dynamicStates = states.SelectFields(parameters.FieldsList);

            return this.Ok(dynamicStates);
        }

        [HttpGet("countries/{countryId:int}/states/{stateId:int}/cities")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WrappedCollection<City>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCitiesByCountryAndStateAsync(int countryId, int stateId, [FromQuery] CollectionQueryParameters parameters)
        {
            var cities = await _geoService.GetCitiesByCountryAndStateAsync(countryId, stateId, parameters);

            var dynamicCities = cities.SelectFields(parameters.FieldsList);

            return this.Ok(dynamicCities);
        }

        [HttpGet("countries/{countryCode}/states/{stateCode}/cities")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WrappedCollection<City>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCitiesByCountryAndStateAsync(string countryCode, string stateCode, [FromQuery] CollectionQueryParameters parameters)
        {
            var cities = await _geoService.GetCitiesByCountryAndStateAsync(countryCode, stateCode, parameters);

            var dynamicCities = cities.SelectFields(parameters.FieldsList);

            return this.Ok(dynamicCities);
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
    }
}
