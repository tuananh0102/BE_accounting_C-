using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Exceptions
{
    /// <summary>
    /// ngoại lệ xác thực dữ liệu
    /// </summary>
    /// Created by: ttanh (30/06/2023)
    public class ValidateException: Exception
    {
        public Dictionary<string, List<string>> ErrorMsgs { get; set; }
        public string? Data { get; set; }

        public ValidateException()
        {
            ErrorMsgs = new Dictionary<string, List<string>>();
            Data = null;
        }
        public ValidateException(string message) : base(message)
        {
            
        }

        public ValidateException(Dictionary<string, List<string>> errorMsgs)
        {
            ErrorMsgs = errorMsgs;
            Data = null;
        }

        public ValidateException(Dictionary<string, List<string>> errorMsgs, string? data)
        {
            ErrorMsgs = errorMsgs;
            Data = data;
        }
    }
}
