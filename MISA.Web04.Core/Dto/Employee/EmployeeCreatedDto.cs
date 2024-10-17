﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using MISA.Web04.Core.Resources.Employee;
using System.Security.AccessControl;
using MISA.Web04.Core.Dto.Employee.CustomAttribute;

namespace MISA.Web04.Core.Dto.Employee
{

    /// <summary>
    /// dto nhân viên
    /// </summary>
    /// Created by: ttanh (30/06/2023)
   
    public class EmployeeCreatedDto
    {

        /// <summary>
        /// mã nhân viên
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [Required(ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.NOT_EMPTY))]
        [MaxLength(20, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.CODE), ResourceType = typeof(EmployeeVN))]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// tên đầy đủ
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [Required(ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.NOT_EMPTY))]
        [MaxLength(100, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.FULLNAME), ResourceType = typeof(EmployeeVN))]
        public string FullName { get; set; }
        /// <summary>
        /// ngày sinh
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [ValidateDate("DATE_OF_BIRTH")]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// giới tính
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        public int? Gender { get; set; }
        /// <summary>
        /// khóa ngoại id của phòng ban
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [Required(ErrorMessage ="id")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// tên phòng ban
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [Required(ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.NOT_EMPTY))]
        [MaxLength(255, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.DEPARTMENT_NAME), ResourceType = typeof(EmployeeVN))]
        public string? DepartmentName { get; set; }
        /// <summary>
        /// chứng minh thư nhân dân
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [MaxLength(25, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.IDENTITY_NUMBER), ResourceType = typeof(EmployeeVN))]
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// ngày cấp
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [ValidateDate("IDENTIFY_DATE")]
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// nơi cấp
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [MaxLength(255, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.IDENTITY_ADDRESS), ResourceType = typeof(EmployeeVN))]
        public string? IdentityAddress { get; set; }
        /// <summary>
        /// chức danh
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [MaxLength(100, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.POSITION), ResourceType = typeof(EmployeeVN))]
        public string? PositionName { get; set; }
        /// <summary>
        /// địa chỉ
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [MaxLength(255, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.ADDRESS), ResourceType = typeof(EmployeeVN))]
        public string? Address { get; set; }
        /// <summary>
        /// số điện thoại
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [MaxLength(50, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.PHONE_NUMBER), ResourceType = typeof(EmployeeVN))]
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// số điện thoại bàn
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [MaxLength(50, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.PHONELANDLINE), ResourceType = typeof(EmployeeVN))]
        public string? PhoneLandline { get; set; }
        /// <summary>
        /// email
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [MaxLength(100, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.EMAIL), ResourceType = typeof(EmployeeVN))]
        [ValidationEmail("EMAIL_VALIDATE")]
        public string? Email { get; set; }
        /// <summary>
        /// số tài khoản ngân hàng
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [MaxLength(25, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.BANK_ACCOUNT), ResourceType = typeof(EmployeeVN))]
        public string? BankAccount { get; set; }
        /// <summary>
        /// tên ngân hàng
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [MaxLength(255, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.BANK_NAME), ResourceType = typeof(EmployeeVN))]
        public string? BankName { get; set; }
        /// <summary>
        /// chi nhánh
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        [MaxLength(255, ErrorMessageResourceType = typeof(EmployeeVN), ErrorMessageResourceName = nameof(EmployeeVN.MAX_LENGTH_FAIL))]
        [Display(Name = nameof(EmployeeVN.BANK_BRANCH), ResourceType = typeof(EmployeeVN))]
        public string? BankBranch { get; set; }

        /// <summary>
        /// là khách hàng
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        public bool? IsCustomer { get; set; }

        /// <summary>
        /// là nhà cung cấp
        /// </summary>
        /// Created by: ttanh (30/06/2023)
        public bool? IsProvider { get; set; }
    }
}