using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Receipts
{
    public class ReceiptExcelDto
    {
        /// <summary>
        /// ngày hạch toán
        /// </summary>
        public DateTime DateAccounting { get; set; }

        /// <summary>
        /// só chứng từ
        /// </summary>
        public string ReceiptCode { get; set; }

        /// <summary>
        /// mô tả chứng từ
        /// </summary>
        public string? ReceiptDescription { get; set; }

        /// <summary>
        /// tổng tiền
        /// </summary>
        public decimal TotalMoney { get; set; }

        /// <summary>
        /// Tên đối tượng
        /// </summary>
        public string? ProviderName { get; set; }
    }
}
