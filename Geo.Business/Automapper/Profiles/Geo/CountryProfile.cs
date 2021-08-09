using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Business.Automapper.Profiles.Geo
{
    public class CountryProfile : Profile
    {


        public CountryProfile()
        {
            this.CreateMap<Domain.Shared.WrappedCollection<Domain.Models.Geo.Country>, Data.Shared.WrappedCollection<Data.Entities.Geo.Country>>().ReverseMap();

            this.CreateMap<Domain.Models.Geo.Country, Data.Entities.Geo.Country>()
                .ForMember(dest => dest.CountryTimeZones, opts => opts.MapFrom(src => src.TimeZones));


            this.CreateMap<Data.Entities.Geo.Country, Domain.Models.Geo.Country>()
                .ForMember(dest => dest.TimeZones, opts => opts.MapFrom(src => src.CountryTimeZones))
                .ForMember(dest => dest.TranslatedName, opts => opts.MapFrom((src, dest, destMember, context) => src.CountryNameTranslations.Where(translation => translation.Language.ToLower() == context.Items["language"].ToString().ToLower()).Select(translation => translation.Value).FirstOrDefault()));

            this.CreateMap<Data.Entities.Geo.CountryTimeZone, Domain.Models.Geo.TimeZone>();
        }
    }    
}
