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
    public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(IUnitOfWork uow) : base(uow)
        {


        }

        public async Task<IEnumerable<BankAccount>> GetListById(Guid? providerId, Guid? bankAccoutId)
        {
            var parameterBankAcccount = new DynamicParameters();
            parameterBankAcccount.Add("@ProviderId", providerId);
            parameterBankAcccount.Add("@BankAccountId", bankAccoutId);
            var bankAccounts = await _uow.Connection.QueryAsync<BankAccount>("Proc_BankAccount_GetByIds", parameterBankAcccount, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return bankAccounts;
        }
    }
}
