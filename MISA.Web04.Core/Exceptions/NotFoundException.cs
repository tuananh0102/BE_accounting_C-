using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Exceptions
{
    /// <summary>
    /// ngoại lệ không tìm thấy
    /// </summary>
    /// Created by: ttanh (30/06/2023)
    public class NotFoundException : Exception
    {
        public List<string> ErrorMsgs { get; set; }

        public NotFoundException(string msg) : base(msg)
        {
        
        }

        public NotFoundException(List<string> errorMsgs)
        {
            ErrorMsgs = errorMsgs;
        }
    }
}
