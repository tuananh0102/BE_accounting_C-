using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Resources.Provider;
using MISA.Web04.Core.Resources.Receipt;
using MISA.Web04.Core.Services;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProviderController : BaseController<ProviderDto, ProviderCreatedDto, ProviderUpdatedDto>
    {

        private readonly IProviderService _providerService;
        public ProviderController(IProviderService providerService) : base(providerService)
        {

            _providerService = providerService;
        }

        /// <summary>
        /// lấy bản ghi theo tìm kiếm phân trang
        /// </summary>
        /// <param name="pagesize">kích thước trang</param>
        /// <param name="pageIndex">chỉ số trang</param>
        /// <param name="querySearch">từ khóa</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pagesize, [FromQuery] int pageIndex, [FromQuery] string? querySearch)
        {
            if (querySearch == null)
            {
                querySearch = "";
            }
                querySearch = querySearch.Trim();

            var (totalRecord, providerDtos) = await _providerService.GetFilter(pagesize, pageIndex, querySearch);
            return StatusCode(StatusCodes.Status200OK, new
            {
                TotalRecord = totalRecord,
                Data = providerDtos
            });
        }


        /// <summary>
        /// tạo mã nhà cung cấp
        /// </summary>
        /// <returns>mã nhà cung cấp</returns>
        /// Created by: ttanh (09/08/2023)
        [HttpGet("NewCode")]
        public async Task<IActionResult> GetNewCode()
        {
            string newCode = await _providerService.GenerateCode();

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
            MemoryStream ms = await _providerService.GetReceiptExcel(querySearch);


            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{ProviderVN.SHEET_NAME}.xlsx");

        }


    }

}
