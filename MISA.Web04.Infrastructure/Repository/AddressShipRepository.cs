using Dapper;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using MISA.Web04.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Repository
{
    public class AddressShipRepository : BaseRepository<AddressShip>, IAddressShipRepository
    {
        public AddressShipRepository(IUnitOfWork uow) : base(uow)
        {
            
        }

        public async Task<IEnumerable<AddressShip>> GetListById(Guid? providerId, Guid? addressShipId)
        {
            var parameterAddressShip = new DynamicParameters();
            parameterAddressShip.Add("@ProviderId", providerId);
            parameterAddressShip.Add("@AddressShipId", addressShipId);
            var addressShips = await _uow.Connection.QueryAsync<AddressShip>("Proc_AddressShip_GetByIds", parameterAddressShip, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return addressShips;

        }
    }
}
