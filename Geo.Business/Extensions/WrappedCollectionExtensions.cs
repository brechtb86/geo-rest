
using Geo.Rest.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Business.Extensions
{
    public static class WrappedCollectionExtensions
    {
        public static WrappedCollection<dynamic> SelectFields<T>(this WrappedCollection<T> source, string pipeSeperatedFields)
        {
            var result = new WrappedCollection<dynamic>(new Collection<dynamic>(), source.TotalCount, source.CurrentPage, source.PageSize);

            var fields = pipeSeperatedFields?.Split("|", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? new string[] { };

            if(fields.Length == 0)
            {
                return source as WrappedCollection<dynamic>;
            }

            foreach (var item in source.Items)
            {
                var dynamicItem = new ExpandoObject();
                var dynamicItemCollection = dynamicItem as ICollection<KeyValuePair<string, object>>;
                       

                foreach (var field in fields)
                {
                    var property = typeof(T).GetProperties().FirstOrDefault(property => property.Name.ToLowerInvariant().Equals(field.ToLowerInvariant()));

                    if(property != null)
                    {
                        dynamicItemCollection.Add(new KeyValuePair<string, object>(property.Name, property.GetValue(item)));
                    }
                }

                result.Items.Add(dynamicItem);
            }

            return result;
        }
    }
}
