using Dapper;
using MISA.Web04.Core.Dto.Accountants;
using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Dto.Receipts;
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
    public class ReceiptRepository : BaseRepository<Receipt>, IReceiptRepository
    {
        private readonly IUnitOfWork _uow;

        public ReceiptRepository(IUnitOfWork uow):base(uow)
        {
            _uow = uow;
        }

        public async Task<(int, IEnumerable<Receipt>)> GetFilter(int pageSize, int pageIndex, string? querySearch, bool? type)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageOffset", (pageIndex - 1)*pageSize);
            parameters.Add("@type", type);
            parameters.Add("@querySearch", querySearch);
            parameters.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var receipts = await _uow.Connection.QueryAsync<Receipt>("Proc_Receipt_GetFilter", parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);
            int totalRecord = parameters.Get<int>("totalRecord");

            return (totalRecord,receipts);
        }

        public override async Task<Receipt> GetByIdAsync(Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ReceiptId", id);
            var receipt = await _uow.Connection.QueryAsync<Receipt, EmployeeReceiptDto, AccountantDto, Receipt>("Proc_Receipt_GetById", (receipt, employee, accountant) =>
            {
                if (receipt.Accountants == null)
                {
                    receipt.Accountants = new List<AccountantDto>();
                }
                receipt.Accountants.Add(accountant);
             
                receipt.Employee = employee;

                return receipt;
            }, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction, splitOn: "EmployeeId, AccountantId");

            var result = receipt.GroupBy(r => r.ReceiptId).Select(g =>
            {
                var receiptGroup = g.First();
                receiptGroup.Accountants = g.Select(r => r.Accountants.FirstOrDefault()).Where(a => a != null).DistinctBy(a => a.AccountantId).ToList();
                receiptGroup.Employee = g.Select(r => r.Employee).Where(e => e != null).FirstOrDefault();
                

                return receiptGroup;
            });

            return result.ElementAt(0);
        }

        public async Task<string> GetMaxCode()
        {
            string maxCode = await _uow.Connection.QueryFirstOrDefaultAsync<string>("Proc_Receipt_GetMaxCode", null, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return maxCode;
        }

        public async Task<IEnumerable<Guid>> GetByListId(List<Guid> listId, bool isNoted)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ids", listId);
            parameters.Add("@isNoted", isNoted);

            string query = $"SELECT r.ReceiptId FROM receipt r WHERE r.ReceiptId IN @ids AND r.IsNoted = @isNoted";

            var ids = await _uow.Connection.QueryAsync<Guid>(query, parameters, transaction: _uow.Transaction);

            return ids;
        }

        //public async Task<IEnumerable<Receipt>> GetListByKeySearchAsync(string? querySearch)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@querySearch", querySearch);
        //    var receipt = await _uow.Connection.QueryAsync<Receipt>("Proc_Receipt_GetListByKey", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
        //    return receipt;
        //}
    }
}
