using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    public class AddressShip : BaseEntity
    {
        public Guid AddressShipId { get; set; }
        public String Address { get; set; }
        public Guid ProviderId { get; set; }
    }
}
