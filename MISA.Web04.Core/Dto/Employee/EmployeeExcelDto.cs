using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Employee
{
    public class EmployeeExcelDto
    {
        /// <summary>
        /// mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// tên đầy đủ
        /// </summary>

        public string FullName { get; set; }
        /// <summary>
        /// giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// chứng minh thư nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// chức danh
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// số tài khoản ngân hàng
        /// </summary>
        public string? BankAccount { get; set; }
        /// <summary>
        /// tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// chi nhánh
        /// </summary>
        public string? BankBranch { get; set; }
    }
}
