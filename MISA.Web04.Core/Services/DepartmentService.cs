using AutoMapper;
using MISA.Web04.Core.Dto.Department;
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
    public class DepartmentService : BaseService<Department,DepartmentDto,DepartmentCreatedDto, DepartmentUpdatedDto> ,IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository, mapper)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion

        #region Method

        /// <summary>
        /// lấy danh sách phòng ban
        /// </summary>
        /// <param name="queryName"></param>
        /// <returns>danh sách phòng ban</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<IEnumerable<DepartmentDto>> GetListServiceAsync(string queryName)
        {
            IEnumerable<Department> departments = await _departmentRepository.GetListAsync(queryName);
            List<DepartmentDto> departmentDtos = new List<DepartmentDto>();
            foreach (Department department in departments)
            {
                DepartmentDto departmentDto = new DepartmentDto
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName
                };
                departmentDtos.Add(departmentDto);
            }
            return departmentDtos;
        }


        /// <summary>
        /// thêm phòng ban
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created by: ttanh (30/06/2023)
        public Task<int> InsertAsync(Department entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// cập nhật phòng ban
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created by: ttanh (30/06/2023)
        public Task<int> UpdateAsync(Department entity, Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
