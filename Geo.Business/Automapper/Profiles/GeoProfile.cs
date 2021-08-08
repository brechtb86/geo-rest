using AutoMapper;
using Geo.Business.Automapper.Converters;
using Geo.Domain.Shared;

namespace Geo.Business.Automapper.Profiles
{
    public class GeoProfile : Profile
    {
        public GeoProfile()
        {
            this.CreateMap<WrappedCollection<Domain.Models.Geo.Country>, WrappedCollection<Data.Entities.Geo.Country>>().ReverseMap();

            this.CreateMap<Domain.Models.Geo.Country, Data.Entities.Geo.Country>()
                .ForMember(dest => dest.Timezones, opts => opts.MapFrom(src => src.TimezonesJson))
                .ForMember(dest => dest.Translations, opts => opts.MapFrom(src => src.TranslationsJson));
            this.CreateMap<Domain.Models.Geo.State, Data.Entities.Geo.State>().ReverseMap();
            this.CreateMap<Domain.Models.Geo.City, Data.Entities.Geo.City>().ReverseMap();


            this.CreateMap<Data.Entities.Geo.Country, Domain.Models.Geo.Country>()
                .ForMember(dest => dest.TimezonesJson, opts => opts.MapFrom(src => src.Timezones))
                .ForMember(dest => dest.Timezones, opts => opts.ConvertUsing(new TimezonesJsonValueConverter(), src => src.Timezones))
                .ForMember(dest => dest.Translations, opts => opts.ConvertUsing(new TranslationsJsonValueConverter(), src => src.Translations));
            this.CreateMap<Data.Entities.Geo.State, Domain.Models.Geo.State>().ReverseMap();
            this.CreateMap<Data.Entities.Geo.City, Domain.Models.Geo.City>().ReverseMap();
        }
    }
}
