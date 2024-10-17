using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Dto.Group;
using MISA.Web04.Core.Interfaces.Services;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class GroupController : BaseController<GroupDto, GroupCreatedDto, GroupUpdateDto>
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService) : base(groupService)
        {
            _groupService = groupService;
        }

        /// <summary>
        /// láy bản ghi theo tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize">kích thước trang</param>
        /// <param name="pageindex">chỉ số trang</param>
        /// <param name="querySearch">từ khóa</param>
        /// <returns>danh sách bản ghi</returns>
        /// Created by: ttanh(17/08/2023)
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? pageSize, [FromQuery] int ? pageindex, [FromQuery] string? querySearch)
        {
            if (querySearch == null)
            {
                querySearch = "";
            }
            querySearch = querySearch.Trim();
            var groups = await _groupService.GetListAsync(querySearch,pageSize,pageindex);
            return StatusCode(StatusCodes.Status200OK, new { Data = groups });
        }
    }
}
