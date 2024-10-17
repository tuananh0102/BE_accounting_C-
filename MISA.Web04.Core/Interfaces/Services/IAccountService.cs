using MISA.Web04.Core.Dto.Account;
using MISA.Web04.Core.Dto.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Services
{
    public  interface IAccountService : IBaseService<AccountDto, AccountCreatedDto, AccountUpdatedDto>
    {
        /// <summary>
        /// lọc tài khoản
        /// </summary>
        /// <param name="isRoot">có là gốc không</param>
        /// <param name="pageIndex">trang thức mấy</param>
        /// <param name="pageSize">kích thước trang</param>
        /// <param name="inputSearch">từ khóa tìm kiếm</param>
        /// <returns></returns>
        /// Created by: ttanh (11/08/2023)
        Task<(int,int, IEnumerable<AccountDto>)> GetListAsync(bool? isRoot, int pageIndex, int pageSize, string? inputSearch);
        /// <summary>
        /// lấy tài khoản con
        /// </summary>
        /// <param name="parentId">id cha</param>
        /// <param name="parentCode">mã cha</param>
        /// <returns>list tài khoản</returns>
        Task<IEnumerable<AccountDto>> GetChildrenAsync(string parentId, string? parentCode);

        /// <summary>
        /// lấy tài khoản đã săp xếp
        /// </summary>
        /// <param name="pageIndex">trang thứ mấy</param>
        /// <param name="pageSize">kích thước trang</param>
        /// <param name="inputSearch">từ khóa tìm kiếm</param>
        /// <returns>list account</returns>
        /// Created by: ttanh (11/08/2023)
        Task<List<IEnumerable<AccountDto>>> GetSortedAccountAsync(int pageIndex, int pageSize, string? inputSearch);

        /// <summary>
        /// lấy tài khoản theo mã cha
        /// </summary>
        /// <param name="pageIndex">trang thứ mấy</param>
        /// <param name="pageSize">kích thước trang</param>
        /// <param name="inputSearch">từ khóa tìm kiếm</param>
        /// <returns>list account</returns>
        /// Created by: ttanh (11/08/2023)
        Task<(int,int,IEnumerable<AccountDto>)> GetListByCodeAsync(int pageIndex, int pageSize, string? inputSearch);

        /// <summary>
        /// lấy tài khoản theo mã của cha
        /// </summary>
        /// <param name="pageIndex">trang thứ mấy</param>
        /// <param name="pageSize">kích thước trang</param>
        /// <returns>list tài khoản</returns>
        /// Created by: ttanh (11/08/2023)
        Task<IEnumerable<AccountDto>> GetListByRootCodeAsync(int pageIndex, int pageSize);

        /// <summary>
        /// láy tài khoản theo đối tượng
        /// </summary>
        /// <param name="obj">đối tượng</param>
        /// <returns>list tài khoản</returns>
        /// Created by: ttanh (11/08/2023)
        Task<IEnumerable<AccountDto>> GetByObjectAsync(int? obj, string? querySearch);
        /// <summary>
        /// cập nhật trạng thái tài khoản theo mã
        /// </summary>
        /// <param name="code">mã</param>
        /// <param name="status">trạng thái</param>
        /// <returns>số bản ghi cập nhật</returns>
        /// Created by: ttanh (11/08/2023)
        Task<int> UpdateStatusByCodeAsync(string code, bool status);

        /// <summary>
        /// cập nhật trạng thái nhiều tài khoản theo mã cha
        /// </summary>
        /// <param name="parentCode">mã cha</param>
        /// <param name="status">trạng thái</param>
        /// <returns>số bản ghi cập nhật</returns>
        /// Created by: ttanh (11/08/2023)
        Task<int> UpdateStatusByCodeMultipleAsync(string parentCode, bool status);

        /// <summary>
        /// xuất excel
        /// </summary>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <returns></returns>
        Task<MemoryStream> GetReceiptExcel(string? querySearch);
    }
}
