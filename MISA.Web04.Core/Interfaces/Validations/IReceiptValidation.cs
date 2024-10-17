using MISA.Web04.Core.Dto.Receipts;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Validations
{
    public interface IReceiptValidation : IBaseValidation<Receipt>
    {
        /// <summary>
        /// kiếm tra ngày hạch toán và ngày phiếu chi
        /// </summary>
        /// <param name="dateAccounting"></param>
        /// <param name="receiptDate"></param>
        /// Created by: ttanh(17/08/2023)
        void CheckValidDate(DateTime dateAccounting, DateTime receiptDate);

        /// <summary>
        /// kiểm tra hạch toán rỗng
        /// </summary>
        /// <param name="accountantList">danh sách hạch toán</param>
        /// Created by: ttanh(17/08/2023)
        void CheckEmtpyAccountants (List<Accountant>? accountantList);

        /// <summary>
        /// kiểm tra điều kiện ghi sổ
        /// </summary>
        /// <param name="receipt">phiếu cần kiểm tra</param>
        /// <returns>throw exception nếu không thỏa mãn</returns>
        /// Created by: ttanh(17/08/2023)
        Task CheckConditionToNoteAsync(Receipt receipt);
    }
}
