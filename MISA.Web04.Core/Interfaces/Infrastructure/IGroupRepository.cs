using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IGroupRepository : IBaseRepository<Group>
    {

        /// <summary>
        /// lấy danh sách nhân viên
        /// </summary>
        /// <param name="querySearch">tên tìm kiếm</param>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <param name="pageIndex">trang thứ mấy</param>
        /// <returns>danh sách  nhóm nhà cung cấp</returns>
        /// Created by: ttanh (29/07/2023)
        Task<IEnumerable<Group>> GetListAsync(string? querySearch, int? pageSize, int? pageIndex);

    }
}
