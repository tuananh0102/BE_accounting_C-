using AutoMapper;
using MISA.Web04.Core.Dto.Group;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Services
{
    public class GroupService : BaseService<Group, GroupDto, GroupCreatedDto, GroupUpdateDto>,IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository, IMapper mapper): base(groupRepository, mapper) 
        {
            _groupRepository = groupRepository;
        }
        public async Task<IEnumerable<GroupDto>> GetListAsync(string? querySearch, int? pageSize, int? pageIndex)
        {
            var groups = await _groupRepository.GetListAsync(querySearch, pageSize, pageIndex);
            var groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);

            return groupDtos;
        }
    }
}
