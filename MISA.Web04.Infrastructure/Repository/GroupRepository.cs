using Dapper;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.Web04.Infrastructure.Repository
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(IUnitOfWork uow):base(uow)
        {
            
        }
        public async Task<IEnumerable<Group>> GetListAsync(string? querySearch, int? pageSize, int? pageIndex)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageOffset", (pageIndex -1) * pageSize);
            parameters.Add("@querySearch", querySearch);
            var groups = await _uow.Connection.QueryAsync<Group>("Proc_Group_GetFilter", parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);
            return groups;
        }

       

    }
}
