using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Group
{
    public class GroupCreatedDto
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Mã nhóm nhà cung cấp
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// Tên nhóm nhà cung cấp
        /// </summary>
        public string GroupName { get; set; }
    }
}
