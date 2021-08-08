using AutoMapper;
using Geo.Domain.Models.Geo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Business.Automapper.Converters
{
    public class TranslationsJsonValueConverter : IValueConverter<string, ICollection<Translation>>
    {
        public ICollection<Translation> Convert(string sourceMember, ResolutionContext context)
        {
            var result = new Collection<Translation>();

            var translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(sourceMember.Replace("\\", ""));

            foreach (var translation in translations)
            {
                result.Add(new Translation
                {
                    TwoLetterIsoCode = translation.Key,
                    Value = translation.Value
                });
            }

            return result;
        }
    }
}
