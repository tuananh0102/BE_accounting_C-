using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Department
{
    public class DepartmentDto
    {
        #region Property

        /// <summary>
        /// id của phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

        #endregion
    }
}
