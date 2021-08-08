using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Business.Automapper.Profiles.Geo
{
    public class CountryProfile : Profile
    {


        public CountryProfile()
        {
            this.CreateMap<Domain.Shared.WrappedCollection<Domain.Models.Geo.Country>, Data.Shared.WrappedCollection<Data.Entities.Geo.Country>>().ReverseMap();

            this.CreateMap<Domain.Models.Geo.Country, Data.Entities.Geo.Country>()
                .ForMember(dest => dest.Timezones, opts => opts.MapFrom(src => src.TimezonesJson))
                .ForMember(dest => dest.Translations, opts => opts.MapFrom(src => src.TranslationsJson));

            this.CreateMap<Data.Entities.Geo.Country, Domain.Models.Geo.Country>()
                .ForMember(dest => dest.TimezonesJson, opts => opts.MapFrom(src => src.Timezones))
                .ForMember(dest => dest.Timezones, opts => opts.ConvertUsing(new TimezonesJsonValueConverter(), src => src.Timezones))
                .ForMember(dest => dest.LocalizedName, opts => opts.ConvertUsing(new TranslationsJsonValueConverter(), src => src.Translations));
        }
    }

    class TranslationsJsonValueConverter : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
        {
            var language = context.Items["language"]?.ToString().ToLower();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(sourceMember.Replace("\\", "")).GetValueOrDefault(language);
        }
    }

    class TimezonesJsonValueConverter : IValueConverter<string, ICollection<Domain.Models.Geo.Timezone>>
    {
        public ICollection<Domain.Models.Geo.Timezone> Convert(string sourceMember, ResolutionContext context)
        {
            var result = new Collection<Domain.Models.Geo.Timezone>();

            var timezones = JsonConvert.DeserializeObject<ICollection<TimezoneJsonObject>>(sourceMember.Replace("\\", ""));

            foreach (var timezone in timezones)
            {
                result.Add(new Domain.Models.Geo.Timezone
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
