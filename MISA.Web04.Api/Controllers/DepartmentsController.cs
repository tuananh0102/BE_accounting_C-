using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Dto.Department;
using MISA.Web04.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        #region Property
        private IDepartmentService _departmentService;
        #endregion
        #region Contructor
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        #endregion

        #region Method
        /// <summary>
        /// lấy toàn bộ danh sách phòng ban
        /// </summary>
        /// <param name="name"></param>
        /// <returns>danh sách phòng ban</returns>
        /// Created by: ttanh (30/06/2023)
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? name)
        {
            if (name == null)
            {
                name = "";
            }
            name = name.Trim();
            IEnumerable<DepartmentDto> departmentDtos = await _departmentService.GetListServiceAsync(name);
            return StatusCode(StatusCodes.Status200OK, new { Data = departmentDtos });
        } 
        #endregion

    }
}
