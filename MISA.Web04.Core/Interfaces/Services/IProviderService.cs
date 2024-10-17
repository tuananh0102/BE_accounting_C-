using MISA.Web04.Core.Dto.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Services
{
    public interface IProviderService : IBaseService<ProviderDto, ProviderCreatedDto, ProviderUpdatedDto>
    {
        /// <summary>
        /// lọc bản ghi
        /// </summary>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <param name="pageIndex">trang thứ mấy</param>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <returns>danh sách nhà cung cấp</returns>
        Task<(int, IEnumerable<ProviderDto>)> GetFilter(int pageSize, int pageIndex, string? querySearch);

        /// <summary>
        /// tạo code mới và không trùng
        /// </summary>
        /// <returns>code mới</returns>
        /// Created by: ttanh (09/08/2023)
        Task<string> GenerateCode();

        /// <summary>
        /// xuất excel
        /// </summary>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <returns></returns>
        Task<MemoryStream> GetReceiptExcel(string? querySearch);

    }
}
