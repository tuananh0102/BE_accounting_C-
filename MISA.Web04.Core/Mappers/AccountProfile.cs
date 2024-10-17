using AutoMapper;
using MISA.Web04.Core.Dto.Account;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Mappers
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
           CreateMap<Account, AccountExcelDto>();
            CreateMap<Account, AccountDto>();
            CreateMap<AccountCreatedDto, Account>();
            CreateMap<AccountUpdatedDto, Account>();
        }
    }
}
