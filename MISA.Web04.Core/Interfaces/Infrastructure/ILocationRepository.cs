using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        /// <summary>
        /// lấy list địa điểm theo từ khóa và bậc
        /// </summary>
        /// <param name="querySearch">từ khóa</param>
        /// <param name="grade">bậc</param>
        /// <returns>list từ khóa</returns>
        /// Create by: ttanh(31/07/2023)
        Task<IEnumerable<Location>> GetListAsync(string? querySearch, int grade, string? parentId);
    }
}
