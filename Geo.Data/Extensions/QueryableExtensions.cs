using Geo.Rest.Data.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Rest.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static WrappedCollection<T> ToWrappedCollection<T>(this IQueryable<T> source, int page, int pageSize)
        {
            var count = source.Count();

            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new WrappedCollection<T>(new Collection<T>(items), count, page, pageSize);
        }
    }
}
