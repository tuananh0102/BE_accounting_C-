using MISA.Web04.Core.Dto.Group;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Services
{
    public interface IGroupService : IBaseService<GroupDto, GroupCreatedDto, GroupUpdateDto>
    {
        /// <summary>
        /// lọc bản ghi 
        /// </summary>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <param name="pageSize">bản ghi trên 1 trang</param>
        /// <param name="pageIndex">só trang</param>
        /// <returns>list group</returns>
        Task<IEnumerable<GroupDto>> GetListAsync(string? querySearch, int? pageSize, int? pageIndex);
    }
}
