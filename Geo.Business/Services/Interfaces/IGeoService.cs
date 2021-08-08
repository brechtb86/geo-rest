using Geo.Domain.Models.Geo;
using Geo.Domain.Models.Query;
using Geo.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Business.Services.Interfaces
{
    public interface IGeoService
    {
        Task<WrappedCollection<Country>> GetCountriesAsync(CollectionQueryParameters parameters);

        //Task<Country> GetCountryByIdAsync(int id);

        //Task<ICollection<State>> GetStatesAsync(CollectionQueryParameters parameters);

        //Task<State> GetStateByIdAsync(int id);

        //Task<ICollection<State>> GetStatesByCountryIdAsync(int countryId);
    }
}
