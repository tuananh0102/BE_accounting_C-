using MISA.Web04.Core.Dto;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>bản ghi</returns>
        /// Created by: ttanh (30/06/2023)
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// lấy bản ghi theo id
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>bản ghi</returns>
        /// Created by: ttanh (17/06/2023)
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// thêm bản ghi
        /// </summary>
        /// <param name="employee">bản ghi mới cần thêm</param>
        /// <returns>só lượng thêm</returns>
        /// Created by: ttanh (30/06/2023)
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// cập nhật bản ghi
        /// </summary>
        /// <param name="entity">thông tin mới cần cập nhật</param>
        /// <param name="id">id của bản ghi</param>
        /// <returns>số lượng cập nhật</returns>
        /// Created by: ttanh (30/06/2023)
        Task<int> UpdateAsync(TEntity entity, Guid id);

        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>số lượng xóa</returns>
        /// Created by: ttanh (30/06/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>số lượng bản ghi bị xóa</returns>
        /// Created by: ttanh (30/06/2023)
        Task<int> DeleteMultipleAsync(List<Guid> ids);

        /// <summary>
        /// thêm nhiều bản ghi
        /// </summary>
        /// <param name="listEntity">dãy bản ghi</param>
        /// <returns></returns>
        Task InsertListAsync(IEnumerable<TEntity> listEntity);

        /// <summary>
        /// lấy bản ghi theo mã
        /// </summary>
        /// <param name="code"></param>
        /// <returns>dối tượng</returns>
        Task<TEntity> GetByCodeAsync(string code);
    /// <summary>
    /// cập nhật nhiều bản ghi
    /// </summary>
    /// <param name="listEntity">list bản ghi</param>
    /// <returns></returns>
        Task UpdateListAsync (IEnumerable<TEntity> listEntity);

        /// <summary>
        /// lấy bẩn ghi theo từ khóa tìm kiếm
        /// </summary>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <returns>list bản ghi</returns>
        public Task<IEnumerable<TEntity>> GetListByKeySearchAsync(string? querySearch);
    }
}
