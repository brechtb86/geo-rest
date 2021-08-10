﻿using Geo.Rest.Domain.Models.Geo;
using Geo.Rest.Domain.Models.Query;
using Geo.Rest.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Business.Services.Interfaces
{
    public interface IGeoService
    {
        Task<WrappedCollection<Country>> GetCountriesAsync(CollectionQueryParameters parameters);

        Task<Country> GetCountryByIdAsync(int countryId, QueryParameters parameters);

        Task<Country> GetCountryByTwoLetterIsoCodeAsync(string twoLetterIsoCode, QueryParameters parameters);

        //Task<ICollection<State>> GetStatesAsync(CollectionQueryParameters parameters);

        //Task<State> GetStateByIdAsync(int id);

        //Task<ICollection<State>> GetStatesByCountryIdAsync(int countryId);

        string GenerateExportScript(string databaseName = "Geo");
    }
}
