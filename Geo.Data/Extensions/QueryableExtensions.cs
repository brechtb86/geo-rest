using Geo.Rest.Data.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace Geo.Rest.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<WrappedCollection<T>> ToWrappedCollectionAsync<T>(this IQueryable<T> source, int page, int pageSize)
        {
            List<T> items;

            var count = source.Count();

            if(source is IQueryable<T> queryableSource)
            {
                items = await queryableSource.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                items = await Task.FromResult(source.Skip((page - 1) * pageSize).Take(pageSize).ToList());
            }            

            return new WrappedCollection<T>(new Collection<T>(items), count, page, pageSize);
        }
    }
}
