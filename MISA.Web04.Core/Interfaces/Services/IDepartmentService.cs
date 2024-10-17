using MISA.Web04.Core.Dto.Department;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Services
{
    public interface IDepartmentService : IBaseService<DepartmentDto, DepartmentCreatedDto, DepartmentUpdatedDto>
    {
        /// <summary>
        /// lấy danh sách phòng ban
        /// </summary>
        /// <param name="queryName">tên tìm kiếm</param>
        /// <returns>danh sách phòng ban</returns>
        /// Created by: ttanh (30/06/2023)
        Task<IEnumerable<DepartmentDto>> GetListServiceAsync(string queryName);
    }
}
