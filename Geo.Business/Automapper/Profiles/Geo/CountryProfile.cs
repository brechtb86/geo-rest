using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Geo.Rest.Business.Automapper.Profiles.Geo
{
    public class CountryProfile : Profile
    {


        public CountryProfile()
        {
            this.CreateMap<Domain.Shared.WrappedCollection<Domain.Models.Geo.Country>, Data.Shared.WrappedCollection<Data.Entities.Geo.Country>>().ReverseMap();

            this.CreateMap<Domain.Models.Geo.Country, Data.Entities.Geo.Country>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.ThreeLetterIsoCode, opts => opts.MapFrom(src => src.ThreeLetterIsoCode))
                .ForMember(dest => dest.NumericCode, opts => opts.MapFrom(src => src.NumericCode))
                .ForMember(dest => dest.TwoLetterIsoCode, opts => opts.MapFrom(src => src.TwoLetterIsoCode))
                .ForMember(dest => dest.PhoneCode, opts => opts.MapFrom(src => src.PhoneCode))
                .ForMember(dest => dest.Capital, opts => opts.MapFrom(src => src.Capital))
                .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.Currency))
                .ForMember(dest => dest.CurrencySymbol, opts => opts.MapFrom(src => src.CurrencySymbol))
                .ForMember(dest => dest.Tld, opts => opts.MapFrom(src => src.Tld))
                .ForMember(dest => dest.Native, opts => opts.MapFrom(src => src.Native))
                .ForMember(dest => dest.Region, opts => opts.MapFrom(src => src.Region))
                .ForMember(dest => dest.SubRegion, opts => opts.MapFrom(src => src.SubRegion))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Emoji, opts => opts.MapFrom(src => src.Emoji))
                .ForMember(dest => dest.EmojiUnicode, opts => opts.MapFrom(src => src.EmojiUnicode))
                .ForMember(dest => dest.CreatedAt, opts => opts.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opts => opts.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Flag, opts => opts.MapFrom(src => src.Flag))
                .ForMember(dest => dest.Cities, opts => opts.Ignore())
                 .ForMember(dest => dest.States, opts => opts.Ignore())
                .ForMember(dest => dest.CountryNameTranslations, opts => opts.Ignore())
                .ForMember(dest => dest.CountryTimeZones, opts => opts.MapFrom(src => src.TimeZones));


            this.CreateMap<Data.Entities.Geo.Country, Domain.Models.Geo.Country>()                
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.TranslatedName, opts => opts.MapFrom(src => src.CountryNameTranslations.Where(translation => translation.Language.ToLower() == Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName).Select(translation => translation.Value).FirstOrDefault()))
                .ForMember(dest => dest.ThreeLetterIsoCode, opts => opts.MapFrom(src => src.ThreeLetterIsoCode))
                .ForMember(dest => dest.NumericCode, opts => opts.MapFrom(src => src.NumericCode))
                .ForMember(dest => dest.TwoLetterIsoCode, opts => opts.MapFrom(src => src.TwoLetterIsoCode))
                .ForMember(dest => dest.PhoneCode, opts => opts.MapFrom(src => src.PhoneCode))
                .ForMember(dest => dest.Capital, opts => opts.MapFrom(src => src.Capital))
                .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.Currency))
                .ForMember(dest => dest.CurrencySymbol, opts => opts.MapFrom(src => src.CurrencySymbol))
                .ForMember(dest => dest.Tld, opts => opts.MapFrom(src => src.Tld))
                .ForMember(dest => dest.Native, opts => opts.MapFrom(src => src.Native))
                .ForMember(dest => dest.Region, opts => opts.MapFrom(src => src.Region))
                .ForMember(dest => dest.SubRegion, opts => opts.MapFrom(src => src.SubRegion))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Emoji, opts => opts.MapFrom(src => src.Emoji))
                .ForMember(dest => dest.EmojiUnicode, opts => opts.MapFrom(src => src.EmojiUnicode))
                .ForMember(dest => dest.CreatedAt, opts => opts.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opts => opts.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Flag, opts => opts.MapFrom(src => src.Flag))
                .ForMember(dest => dest.Language, opts => opts.MapFrom(src => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName))
                .ForMember(dest => dest.TimeZones, opts => opts.MapFrom(src => src.CountryTimeZones));
               
            this.CreateMap<Data.Entities.Geo.CountryTimeZone, Domain.Models.Geo.TimeZone>();
        }
    }
}
