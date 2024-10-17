
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Dto.Location;
using MISA.Web04.Core.Interfaces.Services;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[Controller]")]
    public class LocationController : BaseController<LocationDto, LocationCreatedDto, LocationUpdatedDto>
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService) : base(locationService)
        {
            _locationService = locationService;
        }


        /// <summary>
        /// lấy bản ghi theo tìm kiếm phân trang
        /// </summary>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <param name="grade">bậc thứ mấy</param>
        /// <param name="parentId">id cha</param>
        /// <returns>danh sách bản ghi</returns>
        /// Created by: ttanh(17/08/2023)
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? querySearch, [FromQuery] int grade, [FromQuery] string? parentId)
        {
            if (querySearch == null)
            {
                querySearch = "";
            }
            querySearch = querySearch.Trim();

            var location = await _locationService.GetListAsync(querySearch, grade, parentId);

            return StatusCode(StatusCodes.Status200OK, new { Data = location });
        }
    }
}
