using AutoMapper;
using MISA.Web04.Core.Dto.Group;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Mappers
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupDto>();
            CreateMap<GroupCreatedDto, Group>();
            CreateMap<GroupUpdateDto, Group>();
            CreateMap<GroupProvider, Group>();
        }
    }
}
