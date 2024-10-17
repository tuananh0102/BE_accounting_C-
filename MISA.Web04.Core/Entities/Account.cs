using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    public class Account: BaseEntity
    {
        #region Property
        /// <summary>
        /// khóa chính
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// số tài khoản
        /// </summary>
        public String AccountCode { get; set; }

        /// <summary>
        /// tên tài khoản
        /// </summary>
        public String AccountName { get; set; }

        /// <summary>
        /// tên tiếng anh
        /// </summary>
        public String? AccountEnglishName { get; set; }

        /// <summary>
        /// diễn giải
        /// </summary>
        public  String? AccountDescription { get; set; }

        /// <summary>
        /// trạng thái tài khoản
        /// </summary>
        public bool AccountStatus { get; set; }

        /// <summary>
        /// Tính chất
        /// </summary>
        public int AccountNature { get; set; }

        /// <summary>
        /// id của cha
        /// </summary>
        public String? AccountParentId { get; set; }

        /// <summary>
        /// số tài khoản cha
        /// </summary>
        public String? AccountParentCode { get; set; }

        /// <summary>
        /// tầng của tài khoản
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// có con hay không
        /// </summary>
        public bool IsParent { get; set; }

        /// <summary>
        /// Đối tượng tài khoản
        /// </summary>
        public int? AccountObject { get; set; }

        /// <summary>
        /// có là gốc hay không
        /// </summary>
        public bool IsRoot { get; set; }


        #endregion
    }
}
