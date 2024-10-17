using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Dto.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IProviderExcel
    {
        /// <summary>
        /// xuất excel
        /// </summary>
        /// <param name="providers">list provider</param>
        /// <returns></returns>
        public MemoryStream GetReceiptExcel(IEnumerable<ProviderExcelDto> providers);
    }
}
