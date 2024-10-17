using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Services
{
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeCreatedDto, EmployeeUpdatedDto>
    {
        /// <summary>
        /// lấy danh sách nhân viên
        /// </summary>
        /// <param name="queryName">tên cần tìm</param>
        /// <param name="recordsPerPage">số bản ghi một trang</param>
        /// <param name="page">trang thứ mấy</param>
        /// <returns></returns>
        Task<(int,IEnumerable<EmployeeDto>)> GetListAsync(string? queryName, int? recordsPerPage, int? page);

        /// <summary>
        /// tạo code mới và không trùng
        /// </summary>
        /// <returns>code mới</returns>
        /// Created by: ttanh (30/06/2023)
        Task<string> GenerateCode();

        /// <summary>
        /// xuất excel
        /// </summary>
        /// <returns></returns>
        Task<MemoryStream> GetEmployeeExcelFile();
    }
}
