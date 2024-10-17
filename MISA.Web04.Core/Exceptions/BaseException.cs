using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Exceptions
{
    /// <summary>
    /// ngoại lệ cơ sở
    /// </summary>
    /// Created by: ttanh (30/06/2023)
    public class BaseException
    {
        #region Properties
        public int ErrorCode { get; set; }
        public string? DevMsg { get; set; }
        public string? UserMsg { get; set; }
        public string? TraceId { get; set; }
        public string? MoreInfo { get; set; }
        public Object? ErrorMsgs { get; set; }
        public string? Data { get; set; }

        #endregion

        #region Methods
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion
    }
}
