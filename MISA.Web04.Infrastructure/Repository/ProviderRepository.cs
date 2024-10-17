using Dapper;
using DocumentFormat.OpenXml.Math;
using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.Web04.Infrastructure.Repository
{
    public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
    {
        private readonly IProviderGroupRepository _providerGroupRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IAddressShipRepository _addressShipRepository;

        public ProviderRepository(IUnitOfWork uow, IProviderGroupRepository providerGroupRepository, IBankAccountRepository bankAccountRepository, IAddressShipRepository addressShipRepository) : base(uow)
        {
            _providerGroupRepository = providerGroupRepository;
            _bankAccountRepository = bankAccountRepository;
            _addressShipRepository = addressShipRepository;

        }

        public async Task<(int, IEnumerable<Provider>)> GetFilter(int pageSize, int pageIndex, string? querySearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageOffset", (pageSize * (pageIndex - 1)));
            parameters.Add("@querySearch", querySearch);
            parameters.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var providers = await _uow.Connection.QueryAsync<Provider>("Proc_Provider_GetFilter", parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);
            int totalRecord = parameters.Get<int>("totalRecord");

            return (totalRecord, providers);
        }


        //public async Task<int> InsertRelationshipAsync(Provider provider)
        //{
        //    try
        //    {
        //        await _uow.BeginTransactionAsync();

        //        var groupProvivers = provider.Groups;
        //        var addressShips = provider.AddressShips;
        //        var bankAccounts = provider.BankAccounts;
        //        provider.Groups = null;
        //        provider.AddressShips = null;
        //        provider.BankAccounts = null;
        //        await base.InsertAsync(provider);

        //        var providerId = provider.ProviderId;

        //        foreach (var groupProvider in groupProvivers)
        //        {
        //            groupProvider.ProviderId = providerId;
        //            await _providerGroupRepository.InsertAsync(groupProvider);
        //        }

        //        foreach (var addressShip in addressShips)
        //        {
        //            addressShip.AddressShipId = Guid.NewGuid();
        //            addressShip.ProviderId = providerId;
        //            await _addressShipRepository.InsertAsync(addressShip);
        //        }

        //        foreach (var bankAccount in bankAccounts)
        //        {
        //            bankAccount.BankAccountId = Guid.NewGuid();
        //            bankAccount.ProviderId = providerId;
        //            await _bankAccountRepository.InsertAsync(bankAccount);
        //        }


        //        await _uow.CommitAsync();

        //        return 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        await _uow.RollbackAsync();
        //        throw ex;
        //    }


        //}



        //public async Task<int> DeleteRelationshipAsync(Guid providerId)
        //{
        //    try
        //    {
        //        await _uow.BeginTransactionAsync();
        //        var parametersGroupProvider = new DynamicParameters();
        //        parametersGroupProvider.Add("@ProviderId", providerId);
        //        parametersGroupProvider.Add("@GroupId", null);
        //        var groupProviders = await _uow.Connection.QueryAsync<GroupProvider>("Proc_GroupProvider_GetByIds", parametersGroupProvider, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

        //        foreach (var groupProvider in groupProviders)
        //        {
        //            var parameters = new DynamicParameters();
        //            parameters.Add("@ProviderId", providerId);
        //            parameters.Add("@GroupId", groupProvider.GroupId);
        //            await _uow.Connection.ExecuteAsync("Proc_GroupProvider_Delete", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

        //        }
        //        // 
        //        var parameterBankAcccount = new DynamicParameters();
        //        parameterBankAcccount.Add("@ProviderId", providerId);
        //        parameterBankAcccount.Add("@BankAccountId", null);
        //        var bankAccounts = await _uow.Connection.QueryAsync<BankAccount>("Proc_BankAccount_GetByIds", parameterBankAcccount, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

        //        var bankAccountIds = bankAccounts.Select(b => b.BankAccountId).ToList();

        //        await _bankAccountRepository.DeleteMultipleAsync(bankAccountIds);



        //        var parameterAddressShip = new DynamicParameters();
        //        parameterAddressShip.Add("@ProviderId", providerId);
        //        parameterAddressShip.Add("@AddressShipId", null);
        //        var addressShips = await _uow.Connection.QueryAsync<AddressShip>("Proc_AddressShip_GetByIds", parameterAddressShip, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
        //        var addressShipIds = addressShips.Select(a => a.AddressShipId).ToList();

        //        await _addressShipRepository.DeleteMultipleAsync(addressShipIds);



        //        var paramProvider = new DynamicParameters();
        //        paramProvider.Add("@ProviderId", providerId);
        //        await _uow.Connection.ExecuteAsync("Proc_Provider_Delete", paramProvider, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

        //        await _uow.CommitAsync();

        //        return 1 + groupProviders.Count();
        //    }
        //    catch (Exception ex)
        //    {
        //        await _uow.RollbackAsync();
        //        throw ex;
        //    }
        //}

        //public async Task<int> UpdateRelationshipAsync(Provider provider, Guid providerId, List<Guid> oldIdList, List<Guid> newIdList)
        //{
        //    try
        //    {
        //        await _uow.BeginTransactionAsync();

        //        var groupProvivers = provider.Groups;
        //        var addressShips = provider.AddressShips;
        //        var bankAccounts = provider.BankAccounts;
        //        provider.Groups = null;
        //        provider.AddressShips = null;
        //        provider.BankAccounts = null;

        //        await UpdateAsync(provider, providerId);

        //        for (int i = 0; i < Math.Min(oldIdList.Count, newIdList.Count); i++)
        //        {

        //            var parameters = new DynamicParameters();


        //            // map property của employee với tham số truyền vào database

        //            parameters.Add("@ProviderId", providerId);
        //            parameters.Add("@oldGroupId", oldIdList.ElementAt(i));
        //            parameters.Add("@newGroupId", newIdList.ElementAt(i));


        //            await _uow.Connection.ExecuteAsync($"Proc_GroupProvider_Update", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
        //        }

        //        if (oldIdList.Count > newIdList.Count)
        //        {
        //            for (int i = newIdList.Count; i < oldIdList.Count; i++)
        //            {
        //                var parameters = new DynamicParameters();
        //                parameters.Add("@ProviderId", providerId);
        //                parameters.Add("@GroupId", oldIdList.ElementAt(i));
        //                await _uow.Connection.ExecuteAsync("Proc_GroupProvider_Delete", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
        //            }
        //        }
        //        else if (oldIdList.Count < newIdList.Count)
        //        {
        //            for (int i = oldIdList.Count; i < newIdList.Count; i++)
        //            {
        //                var parameters = new DynamicParameters();
        //                parameters.Add("@ProviderId", providerId);
        //                parameters.Add("@GroupId", newIdList.ElementAt(i));
        //                parameters.Add("@CreatedDate", DateTime.Now);
        //                parameters.Add("@CreatedBy", null);
        //                await _uow.Connection.ExecuteAsync("Proc_GroupProvider_Insert", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
        //            }
        //        }



        //        await _addressShipRepository.DeleteMultipleAsync(new List<Guid> { providerId });
        //        await _bankAccountRepository.DeleteMultipleAsync(new List<Guid> { providerId });

        //        if (addressShips != null)
        //        {
        //            foreach (var addressShip in addressShips)
        //            {
        //                if (addressShip != null)
        //                {
        //                    addressShip.AddressShipId = Guid.NewGuid();
        //                    addressShip.ProviderId = providerId;
        //                    await _addressShipRepository.InsertAsync(addressShip);

        //                }
        //            }
        //        }

        //        if (bankAccounts != null)
        //        {
        //            foreach (var bankAccount in bankAccounts)
        //            {
        //                if (bankAccount != null)
        //                {

        //                    bankAccount.BankAccountId = Guid.NewGuid();
        //                    bankAccount.ProviderId = providerId;
        //                    await _bankAccountRepository.InsertAsync(bankAccount);
        //                }
        //            }
        //        }

        //        await _uow.CommitAsync();
        //        return 1 + newIdList.Count;

        //    }
        //    catch (Exception e)
        //    {
        //        await _uow.RollbackAsync();
        //        throw e;
        //    }
        //}

        public override async Task<Provider> GetByIdAsync(Guid id)
        {
            var paramteters = new DynamicParameters();
            paramteters.Add($"@ProviderId", id);
            var providers = await _uow.Connection.QueryAsync<Provider, GroupProvider, BankAccount, AddressShip, Provider>($"Proc_Provider_GetById", (provider, group, bankAccount, addressShip) =>
            {
                if (provider.Groups == null)
                {
                    provider.Groups = new List<GroupProvider>();
                }
                if (provider.BankAccounts == null)
                {
                    provider.BankAccounts = new List<BankAccount>();
                }
                if (provider.AddressShips == null)
                {
                    provider.AddressShips = new List<AddressShip>();
                }

                provider.Groups.Add(group);



                provider.AddressShips.Add(addressShip);


                provider.BankAccounts.Add(bankAccount);


                return provider;
            }, paramteters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction, splitOn: "GroupId, BankAccountId, AddressShipId");

            var result = providers.GroupBy(p => p.ProviderId).Select(g =>
            {
                var groupedProvider = g.First();
                groupedProvider.Groups = g.Select(p => p.Groups.FirstOrDefault()).Where(a => a != null).DistinctBy(b => b.GroupId).ToList();
                groupedProvider.BankAccounts = g.Select(p => p.BankAccounts.FirstOrDefault()).Where(a => a != null).DistinctBy(b => b.BankAccountId).ToList();
                groupedProvider.AddressShips = g.Select(p => p.AddressShips.FirstOrDefault()).Where(a => a!=null).DistinctBy(b => b.AddressShipId).ToList();

                //groupedProvider.ListGroupCode = g.Select(p => p.Groups.Single().GroupCode).ToList();
                //groupedProvider.ListGroupId = g.Select(p => p.Groups.Single().GroupId).ToList();
                return groupedProvider;
            });

            var tmpGroups = result.ElementAt(0).Groups.Where(r => r != null);
            result.FirstOrDefault().Groups = new List<GroupProvider>();
            if (tmpGroups.Count() > 0)
            {
                result.ElementAt(0).Groups = tmpGroups.ToList();
            }
            
            var tmpBamks = result.ElementAt(0).BankAccounts.Where(b => b != null);
            if (tmpBamks.Count() > 0)
            {
                result.ElementAt(0).BankAccounts = tmpBamks.DistinctBy(b => b.BankAccountId).ToList();
            }
            else
            {
                result.ElementAt(0).BankAccounts = new List<BankAccount>();
            }

            var tmpAddress = result.ElementAt(0).AddressShips.Where(b => b != null);
            if (tmpAddress.Count() > 0)
            {
                result.ElementAt(0).AddressShips = tmpAddress.ToList();
            }
            else
            {
                result.ElementAt(0).AddressShips = new List<AddressShip>();
            }

            return result.ElementAt(0);
        }

        public override async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            try
            {
                await _uow.BeginTransactionAsync();
                var resultDeleteGroupProvider = await _providerGroupRepository.DeleteMultipleAsync(ids);
                await _bankAccountRepository.DeleteMultipleAsync(ids);
                await _addressShipRepository.DeleteMultipleAsync(ids);

                var parameters = new DynamicParameters();
                string id_list = ConvertIdListToString(ids);
                parameters.Add("id_list", id_list);
                //string sql = $"DELETE FROM {tableName} WHERE {tableName}Id IN @ids";
                var resultDeleteProvider = await _uow.Connection.ExecuteAsync($"Proc_Provider_DeleteMultiple", parameters, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);


                await _uow.CommitAsync();
                return resultDeleteGroupProvider + resultDeleteProvider;

            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw ex;
            }
        }

        public async Task<string> GetMaxCode()
        {
            string maxCode = await _uow.Connection.QueryFirstOrDefaultAsync<string>("Proc_Provider_GetMaxCode", null, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return maxCode;
        }
    }
}
