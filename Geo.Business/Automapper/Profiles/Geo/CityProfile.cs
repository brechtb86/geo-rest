using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Geo.Rest.Business.Automapper.Profiles.Geo
{
    class CityProfile : Profile
    {
        public CityProfile()
        {
            this.CreateMap<Domain.Shared.WrappedCollection<Domain.Models.Geo.City>, Data.Shared.WrappedCollection<Data.Entities.Geo.City>>().ReverseMap();

            this.CreateMap<Data.Entities.Geo.City, Domain.Models.Geo.City>()
                // Base members                
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Flag, opts => opts.MapFrom(src => src.Flag))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.WikiDataId, opts => opts.MapFrom(src => src.WikiDataId))
                .ForMember(dest => dest.CreatedAt, opts => opts.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opts => opts.MapFrom(src => src.UpdatedAt))
                // End base members
                ;
        }
    }
}
