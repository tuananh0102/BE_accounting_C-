using MISA.Web04.Core.Dto;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// lấy danh sách nhân viên
        /// </summary>
        /// <param name="queryName">tên tìm kiếm</param>
        /// <param name="recordsPerPage">số bản ghi 1 trang</param>
        /// <param name="page">trang thứ mấy</param>
        /// <returns>danh sách nhân viên</returns>
        /// Created by: ttanh (30/06/2023)
        Task<(int,IEnumerable<Employee>)> GetListAsync(string? queryName, int? recordsPerPage, int? page);


        /// <summary>
        /// kiểm tra mã trùng
        /// </summary>
        /// <param name="code"></param>
        /// <returns>true or fale</returns>
        /// Created by: ttanh (30/06/2023)
       
        Task<bool> IsDuplicateCodeAsync(string code);

        /// <summary>
        /// kiểm tra id tồn tại
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: ttanh (30/06/2023)
        Task<bool> IsExistedIdAsync(Guid id);

        /// <summary>
        /// lấy mã code lớn nhất
        /// </summary>
        /// <returns>mã code lớn nhất</returns>
        /// Created by: ttanh (30/06/2023)
        Task<string> GetMaxCode();

    }
}
