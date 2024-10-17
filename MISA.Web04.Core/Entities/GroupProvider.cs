using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    public class GroupProvider : BaseEntity
    {
        public Guid ProviderId { get; set; }
        public Guid GroupId { get; set; }
        public String? GroupCode { get; set; }
    }
}
