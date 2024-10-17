using MISA.Web04.Core.Dto.Accountants;
using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Receipts
{
    public class ReceiptDto
    {
        #region Properties
        /// <summary>
        /// khóa chính
        /// </summary>
        public Guid ReceiptId { get; set; }

        /// <summary>
        /// só chứng từ
        /// </summary>
        public string ReceiptCode { get; set; }

        /// <summary>
        /// mô tả chứng từ
        /// </summary>
        public string? ReceiptDescription { get; set; }

        /// <summary>
        /// loại phiếu: 0.PHiếu chi, 1. phiếu thu
        /// </summary>
        public bool? ReceiptType { get; set; }

        /// <summary>
        /// khóa ngoại đến bảng employee
        /// </summary>
        public string? EmployeeId { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? ReceiptAddress { get; set; }

        /// <summary>
        /// khóa ngoại
        /// </summary>
        public string? ProviderId { get; set; }

        /// <summary>
        /// mã nhà cung cấp
        /// </summary>
        public string? ProviderCode { get; set; }

        /// <summary>
        /// Tên đối tượng
        /// </summary>
        public string? ProviderName { get; set; }

  

        /// <summary>
        /// tên người nhận
        /// </summary>
        public string? ReceiptReceiver { get; set; }

        /// <summary>
        /// Số lượng kèm theo
        /// </summary>
        public long? ReceiptAttachNumber { get; set; }

        /// <summary>
        /// ngày hạch toán
        /// </summary>
        public DateTime DateAccounting { get; set; }

        /// <summary>
        /// ngày phiếu chi
        /// </summary>
        public DateTime ReceiptDate { get; set; }

        /// <summary>
        /// tổng tiền
        /// </summary>
        public decimal TotalMoney { get; set; }


        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// ngày thay đổi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// ngày thay đổi
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Đã ghi sổ hay chưa: 0. chưa ghi sổ, 1. đã ghi sổ
        /// </summary>
        public bool? IsNoted { get; set; }

        public List<AccountantDto> Accountants { get; set; }
        public EmployeeReceiptDto? Employee { get; set; }
        public ProviderDto? Provider { get; set; }
        #endregion
    }
}
