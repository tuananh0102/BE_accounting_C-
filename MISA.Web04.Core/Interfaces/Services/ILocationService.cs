using MISA.Web04.Core.Dto.Location;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Services
{
    public interface ILocationService : IBaseService<LocationDto, LocationCreatedDto, LocationUpdatedDto>
    {
        /// <summary>
        /// lấy danh sách phòng ban
        /// </summary>
        /// <param name="queryName">tên tìm kiếm</param>
        /// <returns>danh sách đơn vị</returns>
        /// Created by: ttanh (30/06/2023)
        Task<IEnumerable<LocationDto>> GetListAsync(string? querySearch, int grade, string? parentId);
    }
}
