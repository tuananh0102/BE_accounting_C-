using AutoMapper;
using MISA.Web04.Core.Dto.Location;
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
    public class LocationService : BaseService<Location, LocationDto, LocationCreatedDto, LocationUpdatedDto>, ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService(ILocationRepository locationRepository, IMapper mapper):base(locationRepository, mapper)
        {
            _locationRepository = locationRepository;
        }
        public async Task<IEnumerable<LocationDto>> GetListAsync(string? querySearch, int grade, string? parentId)
        {
            var locations = await _locationRepository.GetListAsync(querySearch, grade, parentId);
            var locationDto = _mapper.Map<IEnumerable<LocationDto>>(locations);
            return locationDto;
        }
    }
}
