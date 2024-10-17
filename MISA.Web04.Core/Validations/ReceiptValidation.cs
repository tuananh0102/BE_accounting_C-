using MISA.Web04.Core.Dto.Receipts;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Validations;
using MISA.Web04.Core.Resources.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Validations
{
    public class ReceiptValidation : BaseValidation<Receipt>, IReceiptValidation
    {
        private readonly IReceiptRepository _receiptRepository;

        public ReceiptValidation(IReceiptRepository receiptRepository):base(receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public Task CheckConditionToNoteAsync(Receipt receipt)
        {
           if (receipt.ProviderId == null)
            {
                throw new ValidateException(new Dictionary<String, List<String>> { { "Provider", new List<string> {string.Format(ProviderVN.EMPTY_PROVIDER_ACCOUNTANT, receipt.Accountants[0]?.AccountDebtCode) } } });
            }

           

          
           return Task.CompletedTask;
        }

        public void CheckEmtpyAccountants(List<Accountant>? accountantList)
        {
            if (accountantList == null)
            {
                throw new ValidateException(new Dictionary<String, List<String>> { { "Accountant", new List<string> { ProviderVN.NEDD_DETAIL_DOCUMENT } } });
            }
            foreach (var accountant in accountantList)
            {
                if (accountant.AccountDebtId == null)
                {
                    throw new ValidateException(new Dictionary<String, List<String>> { { "AccountDebtCode", new List<string> { ProviderVN.NOT_EMPTY_DEBT_ACCOUNT } } });

                }
                if (accountant.AccountBalanceId == null)
                {
                    throw new ValidateException(new Dictionary<String, List<String>> { { "AccountBalanceCode", new List<string> { ProviderVN.NOT_EMPTY_BALANCE_ACCOUNT } } });

                }
            }
        }

        public void CheckValidDate(DateTime dateAccounting, DateTime receiptDate)
        {
            if (dateAccounting <  receiptDate)
            {
                throw new ValidateException(new Dictionary<String, List<String>> { { "DateAccounting", new List<string> { ProviderVN.INVALID_DATE_ACCOUNTING } } });
            }
        }
    }
}
