using MISA.Web04.Core.Dto.Group;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Provider
{
    public class ProviderDto
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string ProviderId { get; set; }

        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public string ProviderCode { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string? ProviderTaxCode { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? ProviderPhone { get; set; }

        /// <summary>
        /// Trang web
        /// </summary>
        public string? ProviderWebsite { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public string? EmployeeId { get; set; }

        /// <summary>
        /// Địa chỉ nhà cung cấp
        /// </summary>
        public string? ProviderAddress { get; set; }

        /// <summary>
        /// Trạng thái là tổ chức hoặc cá nhân
        /// </summary>
        public bool ProviderIsPrivate { get; set; }

        /// <summary>
        /// id nhóm nhà cung cấp
        /// </summary>
        public Guid? GroupId { get; set; }

        public List<Guid>? ListGroupId { get; set; }

        public List<GroupProvider>? Groups { get; set; }

       
        public List<BankAccount>? BankAccounts { get; set; }
        public List<AddressShip>? AddressShips { get; set; }
        public string? ProviderCountry { get; set; }
        public string? ProviderCity { get; set; }
        public string? ProviderDistrict { get; set; }
        public string? ProviderVillage { get; set; }


        /// <summary>
        // chứng minh nhân dân
        /// </summary>
        public string? PersonIdentity { get; set; }
        /// <summary>
        /// ngày cấp
        /// </summary>
        public DateTime? PersonIdentityDate { get; set; }
        /// <summary>
        /// nơi cấp
        /// </summary>
        public string? PersonIdentityAddress { get; set; }


        /// <summary>
        /// mã nhà cung cấp  
        /// </summary>
        public string? GroupCode { get; set; }

        public List<String>? ListGroupCode { get; set; }

        public String? EmployeeCode { get; set; }

        public String? FullName { get; set; }

        public String? ContactFullName { get; set; }
        public String? ContactEmail { get; set; }
        public String? ContactPhone { get; set; }
        public String? ContactRepresent { get; set; }
        public String? ContactSalutation { get; set; }
        public String? ContactNote { get; set; }

        public String? RulePaymentCode { get; set; }

        public int? RulePaymentDebtDay { get; set; }
        public decimal? RulePaymentMaxDebt { get; set; }

        public String? AccountPaymentId { get; set; }
        public String? AccountPaymentCode { get; set; }

        public String? AccountReceiveId { get; set; }

        public string? ReceiverName { get; set; }
        /// <summary>
        /// email người nhận hóa đơn
        /// </summary>
        public string? ReceiverEmails { get; set; }
        /// <summary>
        /// điện thoại người nhận hóa đơn
        /// </summary>
        public string? ReceiverPhone { get; set; }



        /// <summary>
        /// Trạng thái là khách hàng hoặc không là khách hàng
        /// </summary>
        public bool? IsCustomer { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public string? AccountId { get; set; }
    }
}
