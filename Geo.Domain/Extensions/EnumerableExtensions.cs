using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Extensions
{
    /// <summary>
    /// Class EnumerableExtensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Converts to collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>ICollection&lt;T&gt;.</returns>
        public static ICollection<T> ToCollection<T>(this IEnumerable<T> source)
        {
            return new Collection<T>(source.ToArray()); ;
        }
    }
}
