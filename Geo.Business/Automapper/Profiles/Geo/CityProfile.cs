using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Business.Automapper.Profiles.Geo
{
    class CityProfile : Profile
    {
        public CityProfile()
        {
            this.CreateMap<Domain.Models.Geo.City, Data.Entities.Geo.City>().ReverseMap();
        }
    }
}
