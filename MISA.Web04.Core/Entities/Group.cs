using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    public class Group :BaseEntity
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

        public List<Provider>? Providers { get; set; }


    }
}
