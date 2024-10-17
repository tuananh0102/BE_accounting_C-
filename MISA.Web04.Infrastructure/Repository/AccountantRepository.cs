using Dapper;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Repository
{
    public class AccountantRepository : BaseRepository<Accountant>, IAccountantRepository
    {
        private readonly IUnitOfWork _uow;

        public AccountantRepository(IUnitOfWork uow):base(uow)
        {
            _uow = uow;
        }

        public async Task<int> DeleteByParentIdAsync(Guid parentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ReceiptId", parentId);
            var result = await _uow.Connection.ExecuteAsync("Proc_Accountant_DeleteByParentId", parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);

            return result;
        }

        public async Task<int> DeleteMultipleByParentIdsAsync(List<Guid> ids)
        {
            var parameters = new DynamicParameters();
            string id_list = ConvertIdListToString(ids);
            parameters.Add("@id_list", id_list);
            //string sql = $"DELETE FROM {tableName} WHERE {tableName}Id IN @ids";
            var result = await _uow.Connection.ExecuteAsync($"Proc_Accountant_DeleteMultipleByParentIds", parameters, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);



            return result;
        }

        public async Task<Accountant> GetByAccountId(Guid accountId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@accountId", accountId);
            var accountant = await _uow.Connection.QueryFirstOrDefaultAsync<Accountant>("Proc_Accountant_GetByAccountId", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return accountant;
        }
    }
}
