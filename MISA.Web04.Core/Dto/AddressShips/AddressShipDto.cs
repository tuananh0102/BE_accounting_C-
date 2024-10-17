using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.AddressShips
{
    public class AddressShipDto
    {
        public Guid AddressShipId { get; set; }
        public String Address { get; set; }
        public Guid ProviderId { get; set; }
    }
}
