using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Accountants
{
    public class AccountantCreatedDto
    {
        #region Properties
     

        /// <summary>
        /// diễn giải
        /// </summary>
        public string? AccountantDescription { get; set; }

        /// <summary>
        /// tài khoản nợ
        /// </summary>
        public Guid AccountDebtId { get; set; }

    

        /// <summary>
        /// tài khoản có
        /// </summary>
        public Guid AccountBalanceId { get; set; }
        
      

        /// <summary>
        /// khóa ngoại liên kết với bảng receipt
        /// </summary>
        public Guid ReceiptId { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public long? AccountantMoney { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// ngày thay đổi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string? ModifiedBy { get; set; }
        #endregion
    }
}
