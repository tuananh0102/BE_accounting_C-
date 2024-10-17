using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Location
{
    public class LocationUpdatedDto
    {
   
        public String LocationName { get; set; }
        public String? ParentId { get; set; }
        public int Grade { get; set; }
    }
}
