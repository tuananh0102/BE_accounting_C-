using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IBankAccountRepository : IBaseRepository<BankAccount>
    {
        /// <summary>
        /// lấy bản ghi theo id
        /// </summary>
        /// <param name="providerId">id nhà cung cấp</param>
        /// <param name="bankAccouintId">id bản ghi tài khoản ngân hàng</param>
        /// <returns>danh sách tài khoản ngân hàng</returns>
        /// Created by: ttanh(16/08/2023)
        Task<IEnumerable<BankAccount>> GetListById(Guid? providerId, Guid? bankAccouintId);
    }
}
