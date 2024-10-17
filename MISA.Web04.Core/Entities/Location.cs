using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    public class Location
    {
        public Guid LocationId { get; set; }
        public String LocationName { get; set; }
        public String? ParentId { get; set; }
        public int Grade { get; set; }
    }
}
