using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Resources.Employee;
using MISA.Web04.Infrastructure.Repository;
using System.Net;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class EmployeesController : BaseController<EmployeeDto, EmployeeCreatedDto, EmployeeUpdatedDto>
    {
        IEmployeeService _employeeService;
        #region Constructor
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {

            _employeeService = employeeService;
        }
        #endregion

        #region Method
        /// <summary>
        /// đưa ra danh sách nhân viên khi tìm kiếm, phân trang
        /// </summary>
        /// <param name="querySearch"></param>
        /// <param name="recordsPerPage"></param>
        /// <param name="page"></param>
        /// <returns>statusCode và danh sách nhân viên</returns>
        /// Created by: ttanh (30/06/2023)
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? querySearch, [FromQuery] int? recordsPerPage, [FromQuery] int? page)
        {

            IEnumerable<EmployeeDto> employees;
            int totalRecord = 0;
            if (querySearch == null)
            {
                querySearch = "";
            }
            querySearch = querySearch.Trim();

            (totalRecord, employees) = await _employeeService.GetListAsync(querySearch, recordsPerPage, page);

            return StatusCode(StatusCodes.Status200OK, new
            {
                TotalRecord = totalRecord,
                Data = employees
            });

        }

        /// <summary>
        /// tạo mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// Created by: ttanh (30/06/2023)
        [HttpGet("NewCode")]
        public async Task<IActionResult> GetNewCode()
        {
            string newCode = await _employeeService.GenerateCode();

            return StatusCode(StatusCodes.Status200OK, new { Data = newCode });
        }

        /// <summary>
        /// xuất file excel
        /// </summary>
        /// <returns>file excel</returns>
        /// Created by: ttanh (30/06/2023)
        [HttpGet("ExcelFile")]
        public async Task<IActionResult> GetExcelFile()
        {
            MemoryStream ms = await _employeeService.GetEmployeeExcelFile();


            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{EmployeeVN.SHEET_NAME}.xlsx");
           
        }


        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>statusCode và số lượng bản ghi bị xóa</returns>
        /// Created by: ttanh (30/06/2023)
        [HttpDelete]
        public override async Task<IActionResult> Delete([FromBody] List<Guid> ids)
        {
            List<Guid> invalidIds = await _employeeService.DeleteMultipleAsync(ids);
            int deletedNum = ids.Count() - invalidIds.Count();
            return StatusCode(200, new { DeletedNum = deletedNum, InvalidIdNum = invalidIds });
        }

        #endregion

    }
}
