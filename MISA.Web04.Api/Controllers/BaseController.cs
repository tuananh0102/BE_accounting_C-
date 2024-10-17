using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Dto;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Services;

namespace MISA.Web04.Api.Controllers
{

    [ApiController]
    public class BaseController<TEntityDto, TEntityCreatedDto, TEntityUpdatedDto> : ControllerBase
    {
        #region Property

        protected IBaseService<TEntityDto, TEntityCreatedDto, TEntityUpdatedDto> _baseService;
        #endregion

        #region Constructor
        public BaseController(IBaseService<TEntityDto, TEntityCreatedDto, TEntityUpdatedDto> baseService)
        {
            _baseService = baseService; 
        }

        #endregion


        #region Method
        /// <summary>
        /// lấy toàn bàn ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bản ghi</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            TEntityDto entityDto = await _baseService.GetByIdAsync(id);
            return StatusCode(StatusCodes.Status200OK, new { Data = entityDto });
            }


        /// <summary>
        /// thêm mới bản ghi
        /// </summary>
        /// <param name="entityCreatedDto"></param>
        /// <returns>số lượng được thêm</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntityCreatedDto entityCreatedDto)
        {


            int result = await _baseService.InsertAsync(entityCreatedDto);
            if (result > 0) return StatusCode(StatusCodes.Status201Created, result);
            else return StatusCode(StatusCodes.Status400BadRequest, result.ToString());

        }


        /// <summary>
        /// cập nhật bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entityUpdatedDto"></param>
        /// <returns>số lượng được cập nhật</returns>
        /// Created by: ttanh (30/06/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TEntityUpdatedDto entityUpdatedDto)
        {

            int result = await _baseService.UpdateAsync(entityUpdatedDto, id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>số lượng bàn ghi bị xóa</returns>
        /// Created by: ttanh (30/06/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            int result = await _baseService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);

        }

        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">danh sách id</param>
        /// <returns>danh sách id chưa bị xóa</returns>
        /// Created by: ttanh(17/08/2023)
        [HttpDelete]
        public virtual async Task<IActionResult> Delete([FromBody] List<Guid> ids)
        {
            var result = await _baseService.DeleteMultipleAsync(ids);
            return StatusCode(StatusCodes.Status200OK, new { Data = result });

        }
        #endregion
        
    }
}
