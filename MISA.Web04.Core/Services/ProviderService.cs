using AutoMapper;
using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using MISA.Web04.Core.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Services
{
    public class ProviderService : BaseService<Provider, ProviderDto, ProviderCreatedDto, ProviderUpdatedDto>, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderGroupRepository _providerGroupRepository;
        private readonly IProviderValidation _providerValidation;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IAddressShipRepository _addressShipRepository;
        private readonly IUnitOfWork _uow;
        private readonly IProviderExcel _providerExcel;

        public ProviderService(IUnitOfWork uow, IProviderExcel providerExcel , IBankAccountRepository bankAccountRepository, IAddressShipRepository addressShipRepository,IProviderRepository providerRepository, IMapper mapper, IProviderGroupRepository providerGroupRepository, IProviderValidation providerValidation) : base(providerRepository, mapper)
        {

            _providerRepository = providerRepository;
            _providerGroupRepository = providerGroupRepository;
            _providerValidation = providerValidation;
            _bankAccountRepository = bankAccountRepository;
            _addressShipRepository = addressShipRepository;
            _uow = uow;
            _providerExcel = providerExcel;

        }
        public async Task<(int, IEnumerable<ProviderDto>)> GetFilter(int pageSize, int pageIndex, string? querySearch)
        {
            var (totalRecord, providers) = await _providerRepository.GetFilter(pageSize, pageIndex, querySearch);
            var providerDtos = _mapper.Map<IEnumerable<ProviderDto>>(providers);

            return (totalRecord, providerDtos);
        }


        public override async Task<int> InsertAsync(ProviderCreatedDto providerCreatedDto)
        {
            try
            {
                await _uow.BeginTransactionAsync();

                await _providerValidation.CheckDuplicatedCodeAsync(providerCreatedDto.ProviderCode, null);
                var provider = _mapper.Map<Provider>(providerCreatedDto);


                var properties = provider.GetType().GetProperties();


                foreach (var property in properties)
                {
                    var name = property.Name;
                    if (name == $"ProviderId")
                    {
                        property.SetValue(provider, Guid.NewGuid());
                    }
                    else if (name == $"CreatedDate")
                    {
                        property.SetValue(provider, DateTime.Now);
                    }
                    else if (name == $"CreatedBy")
                    {
                        property.SetValue(provider, null);
                    }
                }

              

                provider.Groups = null;
                provider.BankAccounts = null;
                provider.AddressShips = null;
                int result = await _providerRepository.InsertAsync(provider);

                if (providerCreatedDto.Groups != null && providerCreatedDto.Groups.Count > 0)
                {
                    var groupProviders = providerCreatedDto.Groups;
                    foreach(var groupProvider in groupProviders)
                    {
                        groupProvider.ProviderId = provider.ProviderId;
                        
                    }

                    await _providerGroupRepository.InsertListAsync(groupProviders);

                }

                if (providerCreatedDto.BankAccounts != null && providerCreatedDto.BankAccounts.Count > 0)
                {
                    foreach (var bank in providerCreatedDto.BankAccounts)
                    {
                        bank.ProviderId = provider.ProviderId;
                        bank.BankAccountId = Guid.NewGuid();
                    }

                    await _bankAccountRepository.InsertListAsync(providerCreatedDto.BankAccounts);

                }
                if (providerCreatedDto.AddressShips != null && providerCreatedDto.AddressShips.Count > 0)
                {
                    foreach (var address in providerCreatedDto.AddressShips)
                    {
                        address.ProviderId = provider.ProviderId;
                        address.AddressShipId = Guid.NewGuid();
                    }
                    await _addressShipRepository.InsertListAsync(providerCreatedDto.AddressShips);

                }

                await _uow.CommitAsync();

                return result;
            } catch(Exception ex)
            {
                await _uow.RollbackAsync();
                throw ex;
            }

        }

        public override async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                await _uow.BeginTransactionAsync();
                await _providerValidation.CheckExistIdAsync(id);
                var provider = await _providerRepository.GetByIdAsync(id);
                int result = 0;




                await _providerGroupRepository.DeleteMultipleAsync(new List<Guid> { id});
                // 

              
                await _bankAccountRepository.DeleteMultipleAsync(new List<Guid> { id });


                await _addressShipRepository.DeleteMultipleAsync(new List<Guid> { id });

                result = await _providerRepository.DeleteAsync(id);

                await _uow.CommitAsync();
                return result;

            } catch(Exception ex)
            {
                _uow.Rollback();
                throw ex;
            }



        }

        public override async Task<int> UpdateAsync(ProviderUpdatedDto providerUpdatedDto, Guid providerId)
        {
            await _providerValidation.CheckExistIdAsync(providerId);

            await _providerValidation.CheckDuplicatedCodeAsync(providerUpdatedDto.ProviderCode, providerId);

            
            var oldListGroupProviders = await _providerGroupRepository.GetListByIdAsync(providerId, null);
            var oldListGroupIds = new List<Guid>();

            if (oldListGroupProviders != null)
            {
                oldListGroupIds.AddRange(oldListGroupProviders.Select(gp => gp.GroupId));
            }

            await _providerGroupRepository.DeleteMultipleAsync(new List<Guid> { providerId });
            await _addressShipRepository.DeleteMultipleAsync(new List<Guid> { providerId });
            await _bankAccountRepository.DeleteMultipleAsync(new List<Guid> { providerId });


          
            if (providerUpdatedDto.Groups != null && providerUpdatedDto.Groups.Count() > 0)
            {
                var groupProviders = providerUpdatedDto.Groups;
                foreach (var groupProvider in groupProviders)
                {
                    groupProvider.ProviderId = providerId;

                }

                await _providerGroupRepository.InsertListAsync(groupProviders);
       
            }
            if (providerUpdatedDto.BankAccounts != null && providerUpdatedDto.BankAccounts.Count() > 0)
            {
                foreach (var bank in providerUpdatedDto.BankAccounts)
                {
                    bank.ProviderId = providerId;
                    bank.BankAccountId = Guid.NewGuid();
                }

                
                await _bankAccountRepository.InsertListAsync(providerUpdatedDto.BankAccounts);
            }
            if (providerUpdatedDto.AddressShips != null && providerUpdatedDto.AddressShips.Count() > 0)
            {
                foreach (var address in providerUpdatedDto.AddressShips)
                {
                    address.ProviderId = providerId;
                    address.AddressShipId = Guid.NewGuid();
                }
               
                await _addressShipRepository.InsertListAsync(providerUpdatedDto.AddressShips);
            }


            var providerMap = _mapper.Map<Provider>(providerUpdatedDto);
            providerMap.Groups = null;
            providerMap.BankAccounts = null;
            providerMap.AddressShips = null;

            int result = await _providerRepository.UpdateAsync(providerMap, providerId);
            return result;
            // loại bỏ những phần giống nhau
        }

        public async Task<string> GenerateCode()
        {
            /*
             * 1. cắt bỏ NCC-
             * 2. loại bỏ số 0 ở đầu
             * 3.cộng thêm 1
             */

            string maxCode = await _providerRepository.GetMaxCode();
            if (maxCode == null)
            {
                return "NCC-1";
            }

            // cắt bỏ NCC-
            string numStr = maxCode.Substring(4);

            int countFirstZero = 0;

            // loại bỏ 0 ở đầu
            for (int i = 0; i < numStr.Length - 1; i++)
            {
                if (numStr[i] == '0')
                {
                    ++countFirstZero;
                }
                else
                {
                    break;
                }
            }

            if (countFirstZero > 0)
            {
                numStr = numStr.Substring(countFirstZero);
            }

            long num = Int64.Parse(numStr);

            ++num;
            numStr = num.ToString();

            // Thêm lại số 0 đã bị xóa
            string newCode = "";
            for (int i = 0; i < countFirstZero; i++)
            {
                newCode += '0';
            }

            newCode = "NCC-" + newCode + numStr;

            return newCode;
        }

        public async Task<MemoryStream> GetReceiptExcel(string? querySearch)
        {
            var providers = await _providerRepository.GetListByKeySearchAsync(querySearch);
            var providerExcel = _mapper.Map<IEnumerable<ProviderExcelDto>>(providers);

            var memoryStream = _providerExcel.GetReceiptExcel(providerExcel);

            return memoryStream;
        }
    }
}
