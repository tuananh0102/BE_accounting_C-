using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        /// <summary>
        /// lọc tài khoản
        /// </summary>
        /// <param name="isRoot">có là nut gốc không</param>
        /// <param name="pageIndex">trang số</param>
        /// <param name="pageSize">kích thước</param>
        /// <param name="inputSearch">từ khóa tìm kiếm</param>
        /// <returns>list account</returns>
        /// Create by: ttanh(11/08/2023)

        Task<(int,int,IEnumerable<Account>)> GetListAsync(bool? isRoot , int pageIndex, int pageSize, string? inputSearch);

        /// <summary>
        /// lấy tài khoản con
        /// </summary>
        /// <param name="parentId">id của cha</param>
        /// <param name="parentCode">mã cha</param>
        /// <returns>list tài khoản</returns>
        /// Create by: ttanh(11/08/2023)
        Task<IEnumerable<Account>> GetChildrenAsync(string parentId, string? parentCode);

        /// <summary>
        /// lấy tài khoản theo đối tượng
        /// </summary>
        /// <param name="obj">đối tượng</param>
        /// <returns>list tài khoản</returns>
        /// Create by: ttanh(11/08/2023)
        Task<IEnumerable<Account>> GetByObject(int? obj, string? querySearch);

        /// <summary>
        /// lấy list theo mã gốc
        /// </summary>
        /// <param name="listcode">list mã </param>
        /// <returns>list tài khoản</returns>
        /// Create by: ttanh(11/08/2023)
        Task<IEnumerable<Account>> GetListByListCodeAsync(List<string> listcode);

        /// <summary>
        /// lấy tài khoản theo mã cha
        /// </summary>
        /// <param name="pageIndex">trang thứ mấy</param>
        /// <param name="pageSize">kích thước trang</param>
        /// <returns>list tài khoản</returns>
        Task<IEnumerable<Account>> GetListByRootCodeAsync(List<string> listCode);

        /// <summary>
        /// lấy bản ghi theo từ khóa tìm kiếm
        /// </summary>
        /// <param name="searchKey">từ khóa</param>
        /// <returns>list bản ghi</returns>
        /// Created by: ttanh (11/08/2023)
        Task<IEnumerable<Account>> GetLIstByKeySearch(string searchKey);

        /// <summary>
        /// cập nhật trạng thái theo mã
        /// </summary>
        /// <param name="code">mã</param>
        /// <param name="status">trạng thái</param>
        /// <returns>số bản ghi cập nhật</returns>
        /// Created by: ttanh (11/08/2023)
        Task<int> UpdateStatusByCodeAsync(string code, bool status);

        /// <summary>
        /// cập nhật trạng thái nhiều bản ghi theo mã cha
        /// </summary>
        /// <param name="code">mã</param>
        /// <param name="status">trạng thái</param>
        /// <returns>số bản ghi cập nhật</returns>
        /// Created by: ttanh (11/08/2023)
        Task<int> UpdateStatusByCodeMultipleAsync(string parentCode, bool status);
    }
}
