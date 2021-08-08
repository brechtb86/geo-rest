using Geo.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<WrappedCollection<T>> ToWrappedCollectionAsync<T>(this IQueryable<T> source, int page, int pageSize)
        {
            var count = source.Count();

            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new WrappedCollection<T>(new Collection<T>(items), count, page, pageSize);
        }
    }
}
