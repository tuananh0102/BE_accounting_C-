using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Validations
{
    public class ProviderValidation : BaseValidation<Provider>, IProviderValidation
    {
        private readonly IProviderRepository _providerRepository;
        public ProviderValidation(IProviderRepository providerRepository) : base(providerRepository)
        {
            _providerRepository = providerRepository;
        }
    }
}
