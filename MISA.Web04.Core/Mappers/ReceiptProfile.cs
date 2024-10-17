using AutoMapper;
using MISA.Web04.Core.Dto.Receipts;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Mappers
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<Receipt, ReceiptExcelDto>();
            CreateMap<Receipt, ReceiptDto>();
            CreateMap<ReceiptCreatedDto, Receipt>();
            CreateMap<ReceiptUpdatedDto, Receipt>();
        }
    }
}
