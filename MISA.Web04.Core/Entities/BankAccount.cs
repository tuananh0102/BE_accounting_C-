using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    public class BankAccount : BaseEntity
    {
        public Guid BankAccountId { get; set; }
        public String? BankAccountNumber { get; set; }
        public String? BankName { get; set; }
        public String? BankCity { get; set; }
        public String? BankBranch { get; set; }
        public Guid ProviderId { get; set; }
    }
}
