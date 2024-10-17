using Dapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Configuration;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {

        public AccountRepository(IUnitOfWork uow):base(uow)
        {
            
        }

        public async Task<(int,int,IEnumerable<Account>)> GetListAsync(bool? isRoot, int pageIndex, int pageSize, string? inputSearch)
        {
           
                var parameters = new DynamicParameters();
                parameters.Add("@inputSearch", inputSearch);
                parameters.Add("@pageSize", pageSize);
      
                parameters.Add("@pageOffset", pageSize * (pageIndex - 1));
                parameters.Add("@isRoot", isRoot);
                parameters.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@totalRoot", dbType: DbType.Int32, direction: ParameterDirection.Output);



            var accounts = await _uow.Connection.QueryAsync<Account>("Proc_Account_Filter", parameters, commandType: CommandType.StoredProcedure, transaction:_uow.Transaction);
                int totalRecord = parameters.Get<int>("totalRecord");
                int totalRoot = parameters.Get<int>("totalRoot");
           
                return (totalRoot, totalRecord, accounts);
            
        }

        public async Task<IEnumerable<Account>> GetChildrenAsync(string parentId, string? parentCode)
        {
           
                var parameters = new DynamicParameters();
                parameters.Add("@parentId", parentId);
                parameters.Add("@parentCode", parentCode);

                var accounts = await _uow.Connection.QueryAsync<Account>("Proc_Account_GetChildren", parameters, commandType: CommandType.StoredProcedure, transaction:_uow.Transaction);
                return accounts;
            
        }

        public async Task<IEnumerable<Account>> GetByObject(int? obj, string? querySearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@object", obj);
            parameters.Add("@querySearch", querySearch);
            var accounts = await _uow.Connection.QueryAsync<Account>("Proc_Account_GetByObject", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            return accounts;
        }

        public async Task<IEnumerable<Account>> GetListByListCodeAsync(List<string> listRootCode)
        {
            var index = 0;
            string query = "";
            if (listRootCode == null || listRootCode.Count == 0)
            {
                return null;
            }
            var parameters = new DynamicParameters();  
            foreach (var code  in listRootCode)
            {
                if (index > 0)
                {
                    query += "UNION ";
                }
                query += "SELECT AccountId,AccountCode,AccountName,AccountEnglishName, AccountDescription,AccountStatus,AccountNature, AccountParentId,Grade,IsParent,IsRoot,CreatedDate,CreatedBy,ModifiedDate,Modifiedby,AccountObject FROM account WHERE AccountCode = ";
                query += $"@code{index} ";

                parameters.Add($"@code{index}", code);
                ++index;
            }
            query += "Order by AccountCode ";

            var accounts = await _uow.Connection.QueryAsync<Account>(query, parameters,transaction: _uow.Transaction);

            return accounts;
        }



        public async Task<IEnumerable<Account>> GetLIstByKeySearch(string searchKey)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@querySearch", searchKey);

            var accounts = await _uow.Connection.QueryAsync<Account>("Proc_Account_Search", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);  
            return accounts;
        }

        public async Task<IEnumerable<Account>> GetListByRootCodeAsync(List<string> listRootCode)
        {
            var index = 0;
            string query = "";
            if (listRootCode == null || listRootCode.Count == 0)
            {
                return null;
            }
            var parameters = new DynamicParameters();
            foreach (var code in listRootCode)
            {
                if (index > 0)
                {
                    query += "UNION ";
                }
                query += "SELECT AccountId,AccountCode,AccountName,AccountEnglishName, AccountDescription,AccountStatus,AccountNature, AccountParentId,Grade,IsParent,IsRoot,CreatedDate,CreatedBy,ModifiedDate,Modifiedby,AccountObject FROM account WHERE AccountCode LIKE ";
                query += $"CONCAT(@code{index}, '%')";

                parameters.Add($"@code{index}", code);
                ++index;
            }
            query += "Order by AccountCode ";

            var accounts = await _uow.Connection.QueryAsync<Account>(query, parameters, transaction: _uow.Transaction);

            return accounts;
        }

        public async Task<int> UpdateStatusByCodeAsync(string code, bool status)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@accountCode", code);
            parameters.Add("@status", status);

            var result = await _uow.Connection.ExecuteAsync("Proc_Account_UpdateStatusByCode", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return result;
        }   

        public async Task<int> UpdateStatusByCodeMultipleAsync(string parentCode, bool status)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@parentCode", parentCode);
            parameters.Add("@status", status);

            var result = await _uow.Connection.ExecuteAsync("Proc_Account_UpdateStatusByCodeMultiple", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return result;
        }
    }
}
