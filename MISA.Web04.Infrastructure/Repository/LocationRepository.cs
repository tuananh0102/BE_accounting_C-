
using Dapper;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Repository
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(IUnitOfWork uow):base(uow)
        {
            
        }

        public async Task<IEnumerable<Location>> GetListAsync(string? querySearch, int grade, string? parentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@querySearch", querySearch);
            parameters.Add("@grade", grade);
            parameters.Add("@parentId", parentId);

            var locations = await _uow.Connection.QueryAsync<Location>("Proc_Location_GetFilter", parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);
            return locations;
        }
    }
}
