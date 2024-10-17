using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.ProviderGroup
{
    public class ProviderGroupDto
    {
        public Guid ProviderId { get; set; }
        public Guid GroupId { get; set; }
        public String? GroupCode { get; set; }

    }
}
