using AutoMapper;
using Microsoft.VisualBasic.FileIO;
using MISA.Web04.Core.Dto.Account;
using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Interfaces.Validations;
using MISA.Web04.Core.Resources.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Services
{
    public class AccountService : BaseService<Account, AccountDto, AccountCreatedDto, AccountUpdatedDto>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountValidation _accountValidation;
        private readonly IAccountExcel _accountExcel;

        public AccountService(IAccountValidation accountValidation, IAccountExcel accountExcel,IAccountRepository accountRepository, IMapper mapper) : base(accountRepository, mapper)
        {
            _accountRepository = accountRepository;
            _accountValidation = accountValidation;
            _accountExcel = accountExcel;
        }

        public async Task<IEnumerable<AccountDto>> GetChildrenAsync(string parentId, string? parentCode)
        {
            var accounts = await _accountRepository.GetChildrenAsync(parentId, parentCode);
            var accountDtos = _mapper.Map<IEnumerable<AccountDto>>(accounts);
            return accountDtos;
        }

        public async Task<(int, int, IEnumerable<AccountDto>)> GetListAsync(bool? isRoot, int pageIndex, int pageSize, string? inputSearch)
        {
            IEnumerable<Account> accounts = new List<Account>();
            int totalRecord = 0;
            int totalRoot = 0;

            (totalRoot, totalRecord, accounts) = await _accountRepository.GetListAsync(isRoot, pageIndex, pageSize, inputSearch);
            var accountDto = _mapper.Map<IEnumerable<AccountDto>>(accounts);
            return (totalRoot, totalRecord, accountDto);
        }

        public async Task<List<IEnumerable<AccountDto>>> GetSortedAccountAsync(int pageIndex, int pageSize, string? inputSearch)
        {
            throw new NotImplementedException();
            //if (inputSearch == null)
            //{
            //    inputSearch = "";
            //}
            //var (total, accountRoots) = await _accountRepository.GetListAsync(true, 1, pageIndex, inputSearch);

            //var tasks = new List<Task<IEnumerable<Account>>>();

            //var results = new List<IEnumerable<Account>>();

            //foreach (var accountRoot in accountRoots)
            //{
            //    var accounts = await _accountRepository.GetChildrenAsync(accountRoot.AccountId.ToString(), accountRoot.AccountCode);
            //    results.Add(accounts);
            //}


            //var data = new List<IEnumerable<AccountDto>>();
            //for (int i = 0; i < results.Count; i++)
            //{
            //    var sortedAccounts = SortedAccount(accountRoots.ElementAt(i), results.ElementAt(i).ToList());
            //    var accountMapper = _mapper.Map<IEnumerable<AccountDto>>(sortedAccounts);
            //    data.Add(accountMapper);
            //}

            //return data;
        }

        public override async Task<int> InsertAsync(AccountCreatedDto accountCreatedDto)
        {


            await ValidateCodeAsync(accountCreatedDto.AccountCode, null, accountCreatedDto.AccountParentId);
            if (accountCreatedDto.AccountParentId != null)
            {

                await _accountValidation.CheckValidCodeAsync(accountCreatedDto.AccountCode, null, new Guid(accountCreatedDto.AccountParentId));
            }
            if (accountCreatedDto.AccountParentId != null)
            {
                await _accountValidation.CheckValidParentAsync(new Guid( accountCreatedDto.AccountParentId));
            }
            if (accountCreatedDto.AccountParentId == null)
            {
                accountCreatedDto.Grade = 1;
                accountCreatedDto.IsRoot = true;
            }
            else
            {
                var parent = await _accountRepository.GetByIdAsync(new Guid(accountCreatedDto.AccountParentId));
                if (!parent.IsParent)
                {
                    parent.IsParent = true;
                    await _accountRepository.UpdateAsync(parent, parent.AccountId);
                }
                accountCreatedDto.Grade = parent.Grade + 1;
                accountCreatedDto.IsRoot = false;
            }

            var result = await base.InsertAsync(accountCreatedDto);

            return result;
        }

        public override async Task<int> DeleteAsync(Guid id)
        {
            await _accountValidation.CheckDeleteAsync(id);
            var children = await _accountRepository.GetChildrenAsync(id.ToString(), null);
            if (children.Count() > 0)
            {
                var errors = new Dictionary<string, List<string>>();
                errors.Add("AccountCode", new List<string> { AccountVN.DELETE_FAIL });
                throw new RelatedDataException(errors);
            }

            var account = await _accountRepository.GetByIdAsync(id);

            if (account.AccountParentId != null)
            {
                var parent = await _accountRepository.GetByIdAsync(new Guid(account.AccountParentId));
                var childrenOfParent = await _accountRepository.GetChildrenAsync(parent.AccountId.ToString(), null);
                if (childrenOfParent.Count() < 2)
                {
                    parent.IsParent = false;
                    await _accountRepository.UpdateAsync(parent, parent.AccountId);
                }

            }

            var result = await base.DeleteAsync(id);

            return result;

        }

        public override async Task<int> UpdateAsync(AccountUpdatedDto accountUpdatedDto, Guid id)
        {
            var children = await _accountRepository.GetChildrenAsync(id.ToString(), null);
            
            var originAccount = await _accountRepository.GetByIdAsync(id);
            if (children.Count() > 0 && originAccount.AccountParentId != accountUpdatedDto.AccountParentId)
            {
                var errors = new Dictionary<string, List<string>>();
                errors.Add("AccountCode", new List<string> { AccountVN.CHANGE_FAIL });
                throw new RelatedDataException(errors);

            }

            if (originAccount.IsParent == true && originAccount.AccountCode != accountUpdatedDto.AccountCode)
            {
                var errors = new Dictionary<string, List<string>>();
                errors.Add("AccountCode", new List<string> {AccountVN.CHANGE_FAIL });
                throw new RelatedDataException(errors);
            }

            await ValidateCodeAsync(accountUpdatedDto.AccountCode, id, accountUpdatedDto.AccountParentId);

            if (accountUpdatedDto.AccountParentId != null)
            {

                await _accountValidation.CheckValidCodeAsync(accountUpdatedDto.AccountCode, id, new Guid(accountUpdatedDto.AccountParentId));
            }



            if (accountUpdatedDto.AccountParentId != null && originAccount.AccountParentId != accountUpdatedDto.AccountParentId)
            {
                if (originAccount.AccountParentId != null)
                {
                    var originParent = await _accountRepository.GetByIdAsync(new Guid(originAccount.AccountParentId));
                    var childrenOfOriginParent = await _accountRepository.GetChildrenAsync(originParent.AccountId.ToString(), null);
                    if (childrenOfOriginParent.Count() < 2)
                    {
                        originParent.IsParent = false;
                        await _accountRepository.UpdateAsync(originParent, originParent.AccountId);
                    }

                }

                var parent = await _accountRepository.GetByIdAsync(new Guid(accountUpdatedDto.AccountParentId));
                if (!parent.IsParent)
                {
                    parent.IsParent = true;
                    await _accountRepository.UpdateAsync(parent, parent.AccountId);
                }
                accountUpdatedDto.Grade = parent.Grade + 1;
                accountUpdatedDto.IsRoot = false;

            }
            else if (accountUpdatedDto.AccountParentId == null && accountUpdatedDto.AccountParentId != originAccount.AccountParentId)
            {
                accountUpdatedDto.Grade = 1;
                accountUpdatedDto.IsRoot = true;
            }



            var result = await base.UpdateAsync(accountUpdatedDto, id);
            return result;

        }

        private async Task ValidateCodeAsync(string code, Guid? id, string? parentId)
        {

            var errors = new Dictionary<string, List<string>>();
            if (parentId != null)
            {
                var parent = await _accountRepository.GetByIdAsync(new Guid(parentId));
                if (parent != null)
                {
                    if (!code.StartsWith(parent.AccountCode))
                    {

                        errors.Add("AccountCode", new List<string> { AccountVN.NEED_STARTWITH_PARENTCODE });
                        throw new ValidateException(errors);
                    }
                }
                else
                {
                    errors.Add("AccountId", new List<string> { AccountVN.NO_EXIST_PARENT });
                    throw new ValidateException(errors);
                }

            }
            var account = await _accountRepository.GetByCodeAsync(code);
            if (account != null && id == null)
            {
                errors.Add("AccountCode", new List<string> { string.Format(AccountVN.EXISTED_ACCOUNT_CODE, code) });
                throw new ValidateException(errors);
            }
            if (account != null && account.AccountId != id)
            {
                errors.Add("ACcountCode", new List<string> { string.Format(AccountVN.EXISTED_ACCOUNT_CODE, code) });
                throw new ValidateException(errors);
            }

        }


        private IEnumerable<Account> SortedAccount(Account parentAccount, List<Account> childrenAccount)
        {
            var stack = new Stack<Account>();
            var results = new List<Account>();
            stack.Push(parentAccount);
            results.Add(parentAccount);

            while (stack.Count > 0 && childrenAccount.Count > 0)
            {
                var parent = stack.Peek();
                for (int i = 0; i < childrenAccount.Count(); i++)
                {
                    if (childrenAccount.ElementAt(i).AccountParentId == parent.AccountId.ToString())
                    {
                        stack.Push(childrenAccount.ElementAt(i));
                        results.Add(childrenAccount.ElementAt(i));
                        childrenAccount.RemoveAt(i);
                        break;
                    }
                    else if (i == childrenAccount.Count() - 1)
                    {
                        stack.Pop();
                    }
                }

            }

            return results;
        }

        public async Task<IEnumerable<AccountDto>> GetByObjectAsync(int? obj, string? querySearch)
        {
            var accounts = await _accountRepository.GetByObject(obj, querySearch);
            var accountDto = _mapper.Map<IEnumerable<AccountDto>>(accounts);

            return accountDto;
        }

        public async Task<(int, int, IEnumerable<AccountDto>)> GetListByCodeAsync(int pageIndex, int pageSize, string? inputSearch)
        {
            
            // tìm kiếm bản ghi theo từ khóa
            var accountInPage = await _accountRepository.GetLIstByKeySearch(inputSearch);

            if (accountInPage == null || accountInPage.Count() == 0)
            {
                return (0, 0, new List<AccountDto>());
            }

            // set chứa các mã của tài khoản ở trên, và tổ tiên của nó
            var listRootCode = new HashSet<String>();

            // lấy tổ tiên của tài khoản đã tìm được
            foreach (var account in accountInPage)
            {
                if (account.AccountCode.Length >= 3)
                {
                    for (int i = 3; i <= account.AccountCode.Length; i++)
                    {

                        listRootCode.Add(account.AccountCode.Substring(0, i));
                    }
                }
            }

            int countRoot = 0; // đếm só lượng gốc
            int countRecord = 0; // đếm số lượng bản ghi
            int startIndex = 0; // đánh dấu chỉ số bắt đầu một trang mới
            int page = 1; // đang đứng ở page mấy

            var accounts = await _accountRepository.GetListByListCodeAsync(listRootCode.ToList());
            var tmpAccount = new List<Account>();
            bool isSelected = false;
            foreach (var account in accounts)
            {
               
                if (account.IsRoot == true)
                {
                    ++countRoot;
                    ++countRecord;
               
                    
                    // nếu đã đọc hết một trang và đang ở trang tiếp theo
                    if (countRoot % pageSize == 1 && countRoot > 1)
                    {
                        ++page;

                        
                        if (page - pageIndex == 1)
                        {
                            isSelected = true;
                            tmpAccount = accounts.ToList().GetRange(startIndex, countRecord -1  - startIndex);
                        }



                        startIndex = countRecord - 1;
                       
                    }
                }
                else
                {
                    ++countRecord;
                }
                if (countRecord == accounts.Count() && isSelected == false && page <= pageIndex)
                {
                    tmpAccount = accounts.ToList().GetRange(startIndex, accounts.Count() - startIndex);
                }

            }

            if (pageIndex > page)
            {
                return (0, 0, new List<AccountDto>());
            }



            var accountDtos = _mapper.Map<IEnumerable<AccountDto>>(tmpAccount);
            return (countRoot, countRecord, accountDtos);
        }

        public async Task<IEnumerable<AccountDto>> GetListByRootCodeAsync(int pageIndex, int pageSize)
        {
            var (t1,t2,rootAccount) = await _accountRepository.GetListAsync(true, pageIndex, pageSize, "");
            var listRootCode = rootAccount.Select(a => a.AccountCode).ToList();

            var accounts = await _accountRepository.GetListByRootCodeAsync(listRootCode);

            var accountDtos = _mapper.Map<IEnumerable<AccountDto>>(accounts);

            return accountDtos;
        }

        public async Task<int> UpdateStatusByCodeAsync(string code, bool status)
        {
            int result;
            if(status)
            {
                await _accountValidation.CheckValidStatusToUseAsync(code);
                 result = await _accountRepository.UpdateStatusByCodeAsync(code, status);
            } else
            {
                var account = await _accountRepository.GetByCodeAsync(code);
                if (account.IsParent)
                {
                    result = await _accountRepository.UpdateStatusByCodeMultipleAsync(code, status);
                } else
                {
                    result = await _accountRepository.UpdateStatusByCodeAsync(code, status);
                }
            }


            return result;
        }

        public async Task<int> UpdateStatusByCodeMultipleAsync(string parentCode, bool status)
        {
            if (status)
            {
                await _accountValidation.CheckValidStatusToUseAsync(parentCode);
            }

            var result = await _accountRepository.UpdateStatusByCodeMultipleAsync(parentCode, status);

            return result;
        }

        public async Task<MemoryStream> GetReceiptExcel(string? querySearch)
        {
            var accounts = await _accountRepository.GetListByKeySearchAsync(querySearch);
            var accountExcel = _mapper.Map<IEnumerable<AccountExcelDto>>(accounts);

            var memoryStream = _accountExcel.GetReceiptExcel(accountExcel);

            return memoryStream;
        }
    }
}
