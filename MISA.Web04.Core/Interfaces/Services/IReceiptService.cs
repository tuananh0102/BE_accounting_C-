using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Dto.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Services
{
    public interface IReceiptService : IBaseService<ReceiptDto, ReceiptCreatedDto,  ReceiptUpdatedDto>
    {
        /// <summary>
        /// lấy bản ghi theo tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize">kích thươc trang</param>
        /// <param name="pageIndex">chỉ số trang</param>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <param name="type">loại phiếu</param>
        /// <returns>danh sách phiếu</returns>
        /// Created by: ttanh(16/08/2023)
        Task<(int, IEnumerable<ReceiptDto>)> GetFilter(int pageSize, int pageIndex, string? querySearch, bool? type);

        /// <summary>
        /// thêm bản ghi
        /// </summary>
        /// <param name="receiptCreatedDto">dữ liệu thêm</param>
        /// <returns>id bản ghi mới</returns>
        /// Created by: ttanh(16/08/2023)
        Task<Guid> InsertAsync(ReceiptCreatedDto receiptCreatedDto);

        /// <summary>
        /// tạo code mới và không trùng
        /// </summary>
        /// <returns>code mới</returns>
        /// Created by: ttanh (07/08/2023)
        Task<string> GenerateCode();

        /// <summary>
        /// xuất excel
        /// </summary>
        /// <param name="querySearch"></param>
        /// <returns></returns>
        Task<MemoryStream> GetReceiptExcel(string? querySearch);

    }
}
