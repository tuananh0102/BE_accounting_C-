using AutoMapper;
using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Mappers
{
    /// <summary>
    /// mapper nhân viên
    /// </summary>
    /// Created by: ttanh (30/06/2023)
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreatedDto, Employee>();
            CreateMap<EmployeeUpdatedDto, Employee>();
            CreateMap<Employee, EmployeeExcelDto>();
           
        }
    }
}
