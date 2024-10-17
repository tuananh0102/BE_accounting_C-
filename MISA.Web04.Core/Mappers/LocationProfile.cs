using AutoMapper;
using MISA.Web04.Core.Dto.Location;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Mappers
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
        CreateMap<Location, LocationDto>();
            CreateMap<LocationCreatedDto, Location>();
            CreateMap<LocationUpdatedDto, Location>();
            
        }
    }
}
