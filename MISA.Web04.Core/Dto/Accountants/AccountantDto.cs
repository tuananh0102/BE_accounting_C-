using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Accountants
{
    public class AccountantDto
    {
        #region Properties
        // <summary>
        /// khóa chính
        /// </summary>
        public Guid AccountantId { get; set; }

        /// <summary>
        /// diễn giải
        /// </summary>
        public string? AccountantDescription { get; set; }

        /// <summary>
        /// tài khoản nợ
        /// </summary>
        public Guid AccountDebtId { get; set; }
        /// <summary>
        /// mã tài khoản nơ
        /// </summary>
        public string? AccountDebtCode { get; set; }

        /// <summary>
        /// tài khoản có
        /// </summary>
        public Guid AccountBalanceId { get; set; }

        /// <summary>
        /// mã tài khoản có
        /// </summary>
        public string? AccountBalanceCode { get; set; }

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

        /// <summary>
        /// cờ đánh dấu để nhận biết là xóa, cập nhập, thêm: -1.Xóa, 0.cập nhật, 1.Thêm, null. không thay đổi
        /// </summary>
        public int? Flag { get; set; }
        #endregion
    }
}
