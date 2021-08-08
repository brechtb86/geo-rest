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
    public class TimezonesJsonValueConverter : IValueConverter<string, ICollection<Timezone>>
    {
        public ICollection<Timezone> Convert(string sourceMember, ResolutionContext context)
        {
            var result = new Collection<Timezone>();

            var timezones = JsonConvert.DeserializeObject<ICollection<TimezoneJsonObject>>(sourceMember.Replace("\\", ""));

            foreach (var timezone in timezones)
            {
                result.Add(new Timezone
                {
                    ZoneName = timezone.ZoneName,
                    GmtOffset = timezone.GmtOffset,
                    GmtOffsetName = timezone.GmtOffsetName,
                    Abbreviation = timezone.Abbreviation,
                    TimezoneName = timezone.TimezoneName

                });
            }

            return result;
        }

        private class TimezoneJsonObject
        {
            [JsonProperty(PropertyName = "zoneName")]
            public string ZoneName { get; set; }
            [JsonProperty(PropertyName = "gmtOffset")]
            public int GmtOffset { get; set; }
            [JsonProperty(PropertyName = "gmtOffsetName")]
            public string GmtOffsetName { get; set; }
            [JsonProperty(PropertyName = "abbreviation")]
            public string Abbreviation { get; set; }
            [JsonProperty(PropertyName = "tzName")]
            public string TimezoneName { get; set; }
        }
    }    
}
