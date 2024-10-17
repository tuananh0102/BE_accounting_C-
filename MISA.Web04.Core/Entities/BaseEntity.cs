using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    /// <summary>
    /// thực thể cơ sở
    /// </summary>
    /// Created by: ttanh (30/06/2023)
    public abstract class BaseEntity
    {
        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// tạo bơi
        /// </summary>
        public string? CreatedBy { get; set; }
        /// <summary>
        /// ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
