using AutoMapper;
using MISA.Web04.Core.Dto.BankAccounts;
using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Mappers
{
    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            CreateMap<BankAccount, BankAccountDto>();
            CreateMap<BankAccountDto, BankAccount>();
            CreateMap<BankAccountUpdateDto, BankAccount>();
            CreateMap<Employee, EmployeeExcelDto>();
        }
    }
}
