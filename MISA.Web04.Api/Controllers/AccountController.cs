using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Dto.Account;
using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Resources.Account;
using MISA.Web04.Core.Resources.Receipt;
using MISA.Web04.Core.Services;

namespace MISA.Web04.Api.Controllers
{

    [Route("api/v1/[controller]")]
    public class AccountController : BaseController<AccountDto, AccountCreatedDto, AccountUpdatedDto>
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// lấy danh sách tìm kiếm, phân trang
        /// </summary>
        /// <param name="inputSearch">từ khóa tìm kiếm</param>
        /// <param name="pageSize">kích thước trang</param>
        /// <param name="pageIndex">chỉ sô trang</param>
        /// <param name="isRoot">có là gốc không</param>
        /// <returns>danh sách bản ghi</returns>
        /// Created by: ttanh(17/08/2023)
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? inputSearch, [FromQuery] int pageSize, [FromQuery] int pageIndex, [FromQuery] bool? isRoot)
        {

            IEnumerable<AccountDto> accounts;
            int totalRecord = 0;
            int totalRoot = 0;
            if (inputSearch == null)
            {
                inputSearch = "";
            }
            inputSearch = inputSearch.Trim();

            (totalRoot,totalRecord, accounts) = await _accountService.GetListAsync(isRoot, pageIndex, pageSize, inputSearch);

            return StatusCode(StatusCodes.Status200OK, new
            {
                TotalRoot = totalRoot,
                TotalRecord = totalRecord,
                Data = accounts
            });

        }

        /// <summary>
        /// lấy con của tài khoản
        /// </summary>
        /// <param name="parentId">id cha</param>
        /// <param name="parentCode">mã cha</param>
        /// <returns>danh sách tài khoản con</returns>
        /// Created by: ttanh(17/08/2023)
        [HttpGet("Children")]
        public async Task<IActionResult> GetChildren([FromQuery] string parentId, [FromQuery] string? parentCode)
        {
            var accounts = await _accountService.GetChildrenAsync(parentId, parentCode);

            return StatusCode(StatusCodes.Status200OK, new {Data =  accounts});
        }

        /// <summary>
        /// láy danh sách tìm kiếm bao gồm cả con
        /// </summary>
        /// <param name="pageIndex">chỉ số trang</param>
        /// <param name="pageSize">kích thước trang</param>
        /// <param name="queryInput">từ khóa</param>
        /// <returns>danh sách tài khoản</returns>
        /// Created by: ttanh(17/08/2023)
        [HttpGet("SortedAccount")]
        public async Task<IActionResult> GetListSortedAccount([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] String? queryInput)
        {
            if (queryInput == null)
            {
                queryInput = "";
            }

            queryInput = queryInput.Trim();
            var (totalRoot, totalRecord,sortedAccount) = await _accountService.GetListByCodeAsync(pageIndex, pageSize, queryInput);

            return StatusCode(StatusCodes.Status200OK, new {TotalRoot = totalRoot, TotalRecord = totalRecord, Data = sortedAccount});
        }


        /// <summary>
        /// lấy tài khoản theo đối tượng
        /// </summary>
        /// <param name="obj">đối tượng</param>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <returns>danh sách tài khoản</returns>
        /// Created by: ttanh(17/08/2023)
        [HttpGet("Object")]
        public async Task<IActionResult> GetByObject([FromQuery] int? obj, [FromQuery] string? querySearch)
        {
            if (querySearch == null)
            {
                querySearch = "";
            }

            querySearch = querySearch.Trim();
            var account = await _accountService.GetByObjectAsync(obj, querySearch);

            return StatusCode(StatusCodes.Status200OK,new {Data = account});
        }

        /// <summary>
        /// lấy tất cả bản ghi kể cả cha, con ở trang nhất định
        /// </summary>
        /// <param name="pageIndex">chỉ số trang</param>
        /// <param name="pageSize">kích thước trang</param>
        /// <returns>danh sách tài khoản</returns>
        /// Created by: ttanh(17/08/2023)
        [HttpGet("ExpandAccount")]
        public async Task<IActionResult> GetListByListRootAccount([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var accounts = await _accountService.GetListByRootCodeAsync(pageIndex, pageSize);
            return StatusCode(StatusCodes.Status200OK, new { Data = accounts });
        }

        /// <summary>
        /// cập nhật trạng thái 1 tài khoản
        /// </summary>
        /// <param name="code">mã tài khoản</param>
        /// <param name="status">trạng thái</param>
        /// <returns>số lượng cập nhật</returns>
        /// Created by: ttanh(17/08/2023)
        [HttpPut("SingleStatus")]
        public async Task<IActionResult> UpdateStatusByCode([FromQuery] string code, [FromQuery] bool status)
        {
            var result = await _accountService.UpdateStatusByCodeAsync(code, status);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// cập nhật trạng thái nhiều bản ghi
        /// </summary>
        /// <param name="parentCode">mã cha</param>
        /// <param name="status">trạng thái</param>
        /// <returns></returns>
        [HttpPut("MultipleStatus")]
        public async Task<IActionResult> UpdateStatusByCodeMultiple([FromQuery] string parentCode, [FromQuery] bool status)
        {
            var result = await _accountService.UpdateStatusByCodeMultipleAsync(parentCode, status);
            return StatusCode(StatusCodes.Status200OK, result);
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
            MemoryStream ms = await _accountService.GetReceiptExcel(querySearch);


            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{AccountVN.SHEET_NAME}.xlsx");

        }

    }

}
