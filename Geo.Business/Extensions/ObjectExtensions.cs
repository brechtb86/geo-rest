using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Geo.Rest.Business.Extensions
{
    public static class ObjectExtensions
    {
        public static dynamic SelectFields<T>(this T source, IEnumerable<string> fields)
        {
            fields = fields != null && fields.Any() ? fields : typeof(T).GetProperties().Select(property => property.Name);

            var dynamicItem = new ExpandoObject();
            var dynamicItemCollection = dynamicItem as ICollection<KeyValuePair<string, object>>;

            foreach (var field in fields)
            {
                var property = typeof(T).GetProperties().FirstOrDefault(property => property.Name.Equals(field, StringComparison.InvariantCultureIgnoreCase));

                if (property != null && property.GetCustomAttribute<JsonIgnoreAttribute>() == null)
                {
                    dynamicItemCollection.Add(new KeyValuePair<string, object>(property.Name, property.GetValue(source)));
                }
            }

            return dynamicItem;
        }
    }
}
