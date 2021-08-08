using AutoMapper;
using Geo.Rest.Business.Services.Interfaces;
using Geo.Rest.Data.Contexts;
using Geo.Rest.Data.Extensions;
using Geo.Rest.Domain.Models.Geo;
using Geo.Rest.Domain.Models.Query;

using Geo.Rest.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Business.Services
{
    public class GeoService : IGeoService
    {
        private readonly GeoContext _geoContext;

        private readonly IMapper _mapper;

        public GeoService(GeoContext geoContext, IMapper mapper)
        {
            this._geoContext = geoContext;
            this._mapper = mapper;
        }

        public async Task<WrappedCollection<Country>> GetCountriesAsync(CountryCollectionQueryParameters parameters)
        {
            var countryEntities = await this._geoContext.Countries.ToWrappedCollectionAsync<Data.Entities.Geo.Country>(parameters.Page, parameters.PageSize);

            return this._mapper.Map<WrappedCollection<Country>>(countryEntities, opts =>
            {
                opts.Items.Add("language", parameters.Language);
            });            
        }

        public async Task<Country> GetCountryByIdAsync(int countryId, CountryItemQueryParameters parameters)
        {            
            var countryEntity = await this._geoContext.FindAsync<Data.Entities.Geo.Country>(countryId);

            if (countryEntity == null)
            {
                return null;
            }

            return this._mapper.Map<Country>(countryEntity, opts =>
            {
                opts.Items.Add("language", parameters.Language);
            });
        }

        public async Task<Country> GetCountryByTwoLetterIsoCodeAsync(string twoLetterIsoCode, CountryItemQueryParameters parameters)
        {
            var countryEntity = await this._geoContext.Countries.FirstOrDefaultAsync(country => country.TwoLetterIsoCode.ToLower() == twoLetterIsoCode.ToLower());

            if (countryEntity == null)
            {
                return null;
            }

            return this._mapper.Map<Country>(countryEntity, opts =>
            {
                opts.Items.Add("language", parameters.Language);
            });
        }

        //public async Task<ICollection<State>> GetStatesAsync(CollectionQueryParameters parameters)
        //{
        //    var stateEntities = await this._geoContext.States.ToListAsync();

        //    return this._mapper.Map<ICollection<State>>(stateEntities);
        //}

        //public async Task<State> GetStateByIdAsync(int id)
        //{
        //    var stateEntity = await this._geoContext.FindAsync<Data.Entities.Geo.State>(id);

        //    if (stateEntity == null)
        //    {
        //        return null;
        //    }

        //    return this._mapper.Map<State>(stateEntity);
        //}

        //public async Task<ICollection<State>> GetStatesByCountryIdAsync(int countryId)
        //{
        //    var stateEntities = await this._geoContext.States.Where(state => state.CountryId == countryId).ToListAsync();

        //    return this._mapper.Map<ICollection<State>>(stateEntities);
        //}
    }
}
