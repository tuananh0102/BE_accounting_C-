using MISA.Web04.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Services
{
    public interface IBaseService <TEntityDto, TEntityCreatedDto, TEntityUpdatedDto>
    {
        /// <summary>
        /// lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>toàn bộ bản ghi</returns>
        /// Created by: ttanh (30/06/2023)
        Task<IEnumerable<TEntityDto>> GetAllAsync();

        /// <summary>
        /// lấy thống tin một nhân viên
        /// </summary>
        /// <param name="id">id của nhân viên</param>
        /// <returns></returns>
        /// Created by: ttanh (30/06/2023)
        Task<TEntityDto> GetByIdAsync(Guid id);

        /// <summary>
        /// xóa nhân viên
        /// </summary>
        /// <param name="id">id của nhân viên<param>
        /// <returns></returns>
        /// Created by: ttanh (30/06/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: ttanh (30/06/2023)
        Task<int> InsertAsync(TEntityCreatedDto entity);

        /// <summary>
        /// cập nhật dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: ttanh (30/06/2023)
        Task<int> UpdateAsync(TEntityUpdatedDto entity, Guid id);

        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>list ids không bị xóa</returns>
        /// Created by: ttanh (30/06/2023)
        Task<List<Guid>> DeleteMultipleAsync(List<Guid> ids);


    }
}
