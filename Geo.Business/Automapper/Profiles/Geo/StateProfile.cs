using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Business.Automapper.Profiles.Geo
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            this.CreateMap<Domain.Models.Geo.State, Data.Entities.Geo.State>().ReverseMap();
        }
    }
}
