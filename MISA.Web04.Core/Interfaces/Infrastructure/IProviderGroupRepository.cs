using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IProviderGroupRepository : IBaseRepository<GroupProvider>
    {
        /// <summary>
        /// lấy danh sách nhóm nhà cung cấp
        /// </summary>
        /// <param name="providerId">id nhà cung cấp</param>
        /// <param name="groupId">id nhóm nhà cung cấp</param>
        /// <returns>danh sách nhóm nhà cung capas</returns>
        /// Created by: ttanh(16/08/2023)
        Task<IEnumerable<GroupProvider>> GetListByIdAsync(Guid? providerId, Guid? groupId);
    }
}
