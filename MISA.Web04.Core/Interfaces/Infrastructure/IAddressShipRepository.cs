using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IAddressShipRepository : IBaseRepository<AddressShip>
    {
        /// <summary>
        /// lấy danh sách theo id
        /// </summary>
        /// <param name="providerId">id nhà cung cấp</param>
        /// <param name="addressShipId">id bản ghi địa chỉ giao hàng</param>
        /// <returns>danh sách địa chỉ giao hàng</returns>
        /// Created by: ttanh(16/08/2023)
        Task<IEnumerable<AddressShip>> GetListById(Guid? providerId, Guid? addressShipId );

    }
}
