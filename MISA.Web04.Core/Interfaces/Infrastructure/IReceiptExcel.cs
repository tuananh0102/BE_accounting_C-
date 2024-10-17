using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Dto.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IReceiptExcel
    {
        /// <summary>
        /// xuất file excel
        /// </summary>
        /// <param name="receipts">list phiếu</param>
        /// <returns></returns>
        /// Created by: ttanh (13/08/2023)
        public MemoryStream GetReceiptExcel(IEnumerable<ReceiptExcelDto> receipts);
    }
}
