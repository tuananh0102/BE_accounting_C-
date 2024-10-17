using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IAccountantRepository : IBaseRepository<Accountant>
    {
        /// <summary>
        /// xóa bản ghi theo id cha
        /// </summary>
        /// <param name="parentId">id cha</param>
        /// <returns>số lượng bị xóa</returns>
        /// Created by: ttanh(16/08/2023)
        Task<int> DeleteByParentIdAsync(Guid parentId);
        /// <summary>
        /// xóa bản ghi theo nhiêu id chá
        /// </summary>
        /// <param name="ids">danh sách id cha</param>
        /// <returns>số lượng bị xóa</returns>
        /// Created by: ttanh(16/08/2023)
        Task<int> DeleteMultipleByParentIdsAsync(List<Guid> ids);

        /// <summary>
        /// lấy bản ghi theo id của tài khoản
        /// </summary>
        /// <param name="accountId">id tài khoản</param>
        /// <returns>bản ghi</returns>
        /// Created by: ttanh(16/08/2023)
        Task<Accountant> GetByAccountId(Guid accountId);
    }
}
