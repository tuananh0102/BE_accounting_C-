using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Exceptions
{
    public class RelatedDataException : Exception
    {
        public Dictionary<string, List<string>> ErrorMsgs { get; set; }
        public RelatedDataException(string message) : base(message)
        {

        }

        public RelatedDataException(Dictionary<string, List<string>> errorMsgs)
        {
            ErrorMsgs = errorMsgs;
        }
    }
}
