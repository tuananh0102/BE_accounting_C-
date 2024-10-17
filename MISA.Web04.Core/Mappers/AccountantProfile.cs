using AutoMapper;
using MISA.Web04.Core.Dto.Accountants;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Mappers
{
    public class AccountantProfile : Profile
    {
        public AccountantProfile()
        {
            CreateMap<Accountant, AccountantDto>();
            CreateMap<AccountantDto, Accountant>();
            CreateMap<AccountantCreatedDto, Accountant>();
            CreateMap<AccountantUpdatedDto, Accountant>();
        }
    }
}
