using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    /// <summary>
    /// thực thể đơn vị
    /// </summary>
    /// Created by: ttanh (30/06/2023)
    public class Department : BaseEntity
    {
        /// <summary>
        /// id của phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

        
    }
}
