using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Geo.Rest.Business.Automapper.Profiles.Geo
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            this.CreateMap<Domain.Shared.WrappedCollection<Domain.Models.Geo.State>, Data.Shared.WrappedCollection<Data.Entities.Geo.State>>().ReverseMap();

            this.CreateMap<Data.Entities.Geo.State, Domain.Models.Geo.State>()
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
                .ForMember(dest => dest.FipsCode, opts => opts.MapFrom(src => src.FipsCode))
                .ForMember(dest => dest.IsoCode, opts => opts.MapFrom(src => src.IsoCode));
        }
    }
}
