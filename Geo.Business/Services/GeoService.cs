using AutoMapper;
using Geo.Business.Services.Interfaces;
using Geo.Data.Contexts;
using Geo.Data.Extensions;
using Geo.Domain.Models.Geo;
using Geo.Domain.Models.Query;

using Geo.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Business.Services
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

        public async Task<WrappedCollection<Country>> GetCountriesAsync(CollectionQueryParameters parameters)
        {
            var countryEntities = await this._geoContext.Countries.ToWrappedCollectionAsync<Data.Entities.Geo.Country>(parameters.Page, parameters.PageSize);

            return this._mapper.Map<WrappedCollection<Country>>(countryEntities);            
        }

        //public async Task<Country> GetCountryByIdAsync(int id)
        //{
        //    var countryEntity = await this._geoContext.FindAsync<Data.Entities.Geo.Country>(id);

        //    if (countryEntity == null)
        //    {
        //        return null;
        //    }

        //    return this._mapper.Map<Country>(countryEntity);
        //}

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
