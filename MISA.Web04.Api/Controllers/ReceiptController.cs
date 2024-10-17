
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Dto.Receipts;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Resources.Employee;
using MISA.Web04.Core.Resources.Receipt;
using MISA.Web04.Core.Services;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[Controller]")]
    public class ReceiptController : BaseController<ReceiptDto, ReceiptCreatedDto, ReceiptUpdatedDto>
    {
        private readonly IReceiptService _receiptService;
        public ReceiptController(IReceiptService receiptService) : base(receiptService)
        {
            _receiptService = receiptService;
        }

        /// <summary>
        /// láy bản ghi theo tìm kiếm phân trang
        /// </summary>
        /// <param name="pagesize">kích thước trang</param>
        /// <param name="pageIndex">chỉ số trang</param>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <param name="type">loại phiếu</param>
        /// <returns>danh sách bản ghi</returns>
        /// Created by: ttanh(16/08/2023)
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pagesize, [FromQuery] int pageIndex, [FromQuery] string? querySearch, [FromQuery] bool? type)
        {
            if (querySearch == null)
            {
                querySearch = "";
            }
                querySearch = querySearch.Trim();

            var (totalRecord, receiptDto) = await _receiptService.GetFilter(pagesize, pageIndex, querySearch, type);
            return StatusCode(StatusCodes.Status200OK, new
            {
                TotalRecord = totalRecord,
                Data = receiptDto
            });
        }

        /// <summary>
        /// thêm bản ghi
        /// </summary>
        /// <param name="entityCreatedDto">dữ liệu thêm</param>
        /// <returns>id của bản ghi được thêm</returns>
        [HttpPost]
        public override async Task<IActionResult> Post([FromBody] ReceiptCreatedDto entityCreatedDto)
        {


            Guid id = await _receiptService.InsertAsync(entityCreatedDto);
            return StatusCode(StatusCodes.Status201Created, new {Data = id});
            

        }

        /// <summary>
        /// tạo mã phiếu mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// Created by: ttanh (07/08/2023)
        [HttpGet("NewCode")]
        public async Task<IActionResult> GetNewCode()
        {
            string newCode = await _receiptService.GenerateCode();

            return StatusCode(StatusCodes.Status200OK, new { Data = newCode });
        }


        /// <summary>
        /// xuất excel
        /// </summary>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <returns>file excel</returns>
        /// Created by: ttanh (16/08/2023)
        [HttpGet("ExcelFile")]
        public async Task<IActionResult> GetExcelFile([FromQuery] string? querySearch)
        {
            if (querySearch == null)
            {
                querySearch = "";
            }

            querySearch = querySearch.Trim();
            MemoryStream ms = await _receiptService.GetReceiptExcel(querySearch);


            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{ReceiptVN.SHEET_NAME}.xlsx");

        }

    }
}
