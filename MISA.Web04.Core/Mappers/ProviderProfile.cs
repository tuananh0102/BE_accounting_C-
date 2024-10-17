using AutoMapper;
using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Mappers
{
    public class ProviderProfile : Profile
    {
        public ProviderProfile()
        {
            CreateMap<Provider, ProviderExcelDto>();
            CreateMap<Provider, ProviderDto>();
            CreateMap<ProviderCreatedDto, Provider>();
            CreateMap<ProviderUpdatedDto, Provider>();
        }
    }
}
