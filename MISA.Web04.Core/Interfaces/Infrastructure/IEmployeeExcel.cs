using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IEmployeeExcel
    {
        /// <summary>
        /// lấy dữ liệu từ db chuyển thành excel
        /// </summary>
        /// <returns>bảng excel</returns>
        /// Created by: ttanh (30/06/2023)
        public MemoryStream GetEmployeeExcel(IEnumerable<EmployeeExcelDto> employees);
    }
}
