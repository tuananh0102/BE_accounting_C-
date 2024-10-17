using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Validations;
using MISA.Web04.Core.Resources.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Validations
{
    public class AccountValidation : BaseValidation<Account>, IAccountValidation
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountantRepository _accountantRepository;

        public AccountValidation(IAccountRepository accountRepository, IAccountantRepository accountantRepository) : base(accountRepository)
        {
            _accountRepository = accountRepository;
            _accountantRepository = accountantRepository;
        }

        public async Task CheckDeleteAsync(Guid accountId)
        {
            var accountant = await _accountantRepository.GetByAccountId(accountId);
            if (accountant != null)
            {
                throw new ValidateException(new Dictionary<String, List<String>> { { $"Account", new List<string> { AccountVN.NO_DELETE } } });
            }
        }

        public async Task CheckValidCodeAsync(string code, Guid? id, Guid parentId)
        {
            var listCheckCode = new List<string>();
            if (code.Length > 3)
            {
                for (int i = 4; i <= code.Length ; i++)
                {
                    listCheckCode.Add(code.Substring(0, i));
                }
            }
            var accounts = await _accountRepository.GetChildrenAsync(parentId.ToString(), null);
            var accountParent = await _accountRepository.GetByIdAsync(parentId);

            foreach(var account in accounts)
            {
                if (account.AccountCode.StartsWith(code) || code.StartsWith(account.AccountCode)) {
                    if (code.Length < account.AccountCode.Length)
                    {
                        throw new ValidateException(new Dictionary<String, List<String>> { { $"AccountCode", new List<string> { string.Format(AccountVN.NOT_START_WITH_FAIL,code,account.AccountCode) } } });

                    } else
                    {
                        throw new ValidateException(new Dictionary<String, List<String>> { { $"AccountCode", new List<string> { string.Format(AccountVN.NOT_START_WITH_FAIL, account.AccountCode, account.AccountCode) } } });

                    }
                }
            }
  
            
        }

        public async Task CheckValidParentAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            if (account == null)
            {
                throw new NotFoundException(AccountVN.NOT_FOUND);
            }

            if (account.AccountStatus == false)
            {
                throw new ValidateException(new Dictionary<String, List<String>> { { $"AccountParent", new List<string> {string.Format(AccountVN.CANT_CHOOSE_STOP, account.AccountCode) } } });
            }

        }

        public async Task CheckValidStatusToUseAsync(string code)
        {
            if (code.Length > 3)
            {
               var parentCode = code.Substring(0, code.Length - 1);

                var account = await _accountRepository.GetByCodeAsync(parentCode);
                if (account.AccountStatus == false)
                {
                    throw new ValidateException(new Dictionary<String, List<String>> { { $"AccountCode", new List<string> { string.Format(AccountVN.CANT_CHOOSE_STOP_FOR_CHILD, account.AccountCode, code) } } });

                }
            }
        }
    }
}
