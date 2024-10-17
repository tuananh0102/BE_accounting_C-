using AutoMapper;
using MISA.Web04.Core.Dto.Department;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Mappers
{
    /// <summary>
    /// mapper phòng ban
    /// </summary>
    /// Created by: ttanh (30/06/2023)
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
           CreateMap<Department, DepartmentDto>();
        }
    }
}
