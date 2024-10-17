using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Account
{
    public class AccountExcelDto
    {
        /// <summary>
        /// số tài khoản
        /// </summary>
        public String AccountCode { get; set; }

        /// <summary>
        /// tên tài khoản
        /// </summary>
        public String AccountName { get; set; }

        /// <summary>
        /// Tính chất
        /// </summary>
        public int AccountNature { get; set; }

        /// <summary>
        /// tên tiếng anh
        /// </summary>
        public String? AccountEnglishName { get; set; }

        /// <summary>
        /// diễn giải
        /// </summary>
        public String? AccountDescription { get; set; }

        /// <summary>
        /// trạng thái tài khoản
        /// </summary>
        public bool AccountStatus { get; set; }

        


        /// <summary>
        /// tầng của tài khoản
        /// </summary>
        public int Grade { get; set; }
    }
}
