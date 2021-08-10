using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Extensions
{
    public static class ObjectExtensions
    {
        public static dynamic SelectFields<T>(this T source, string pipeSeperatedFields)
        {
            var fields = pipeSeperatedFields?.Split("|", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? typeof(T).GetProperties().Select(property => property.Name);

            var dynamicItem = new ExpandoObject();
            var dynamicItemCollection = dynamicItem as ICollection<KeyValuePair<string, object>>;

            foreach (var field in fields)
            {
                var property = typeof(T).GetProperties().FirstOrDefault(property => property.Name.ToLowerInvariant().Equals(field.ToLowerInvariant()));

                if (property != null && property.GetCustomAttribute<JsonIgnoreAttribute>() == null)
                {
                    dynamicItemCollection.Add(new KeyValuePair<string, object>(property.Name, property.GetValue(source)));
                }
            }

            return dynamicItem;
        }
    }
}
