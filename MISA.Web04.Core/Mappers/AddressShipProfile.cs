using AutoMapper;
using MISA.Web04.Core.Dto.AddressShips;
using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Mappers
{
    public class AddressShipProfile : Profile
    {
        public AddressShipProfile()
        {
            CreateMap<AddressShip, AddressShipDto>();
            CreateMap<AddressShipCreatedDto, AddressShip>();
            CreateMap<AddressShipUpdatedDto, AddressShip>();
         
        }
    }
}
