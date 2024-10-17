using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Provider
{
    public class ProviderExcelDto
    {
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public string ProviderCode { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Địa chỉ nhà cung cấp
        /// </summary>
        public string? ProviderAddress { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string? ProviderTaxCode { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? ProviderPhone { get; set; }

        /// <summary>
        /// Trang web
        /// </summary>
        public string? ProviderWebsite { get; set; }

     
    }
}
