using AutoMapper;
using MISA.Web04.Core.Dto;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Resources.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MISA.Web04.Core.Resources;
using MISA.Web04.Core.Interfaces.Validations;
using System.ComponentModel.DataAnnotations;
using MISA.Web04.Core.Resources.Account;

namespace MISA.Web04.Core.Services
{
    public abstract class BaseService<TEntity,TEntityDto, TEntityCreatedDto, TEntityUpdatedDto> : IBaseService<TEntityDto, TEntityCreatedDto, TEntityUpdatedDto>
    {
        #region Properties
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
       
        private string _tableName = typeof(TEntity).Name;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            
        }
        #endregion

        #region Methods

        /// <summary>
        /// Xóa 1 bản gi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>số lượng bản ghi bị xóa</returns>
        /// Created by: ttanh (30/06/2023)
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            int result = await _baseRepository.DeleteAsync(id);

            return result;

        }

        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created by: ttanh (30/06/2023)
        public virtual async Task<List<Guid>> DeleteMultipleAsync(List<Guid> ids)
        {
            var result = await _baseRepository.DeleteMultipleAsync(ids);

            if (result > 0)
            {
                return null;
            }

            return ids;
        }

        /// <summary>
        /// lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>danh sách bản ghi</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            IEnumerable<TEntity> entities = await _baseRepository.GetAllAsync();
            IEnumerable<TEntityDto> entityDtos = _mapper.Map<IEnumerable<TEntityDto>>(entities);
            return entityDtos;
        }

        /// <summary>
        /// lấy bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// Created by: ttanh (30/06/2023)
        public async Task<TEntityDto> GetByIdAsync(Guid id)
        {
            TEntity entity = await _baseRepository.GetByIdAsync(id);
            string a = TestMethod();
            if (entity == null)
            {
                throw new NotFoundException(new List<string> { AccountVN.NOT_FOUND });
            }
            TEntityDto entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        public virtual string TestMethod()
        {
            return "Base";
        }

        /// <summary>
        /// thêm  mới bản ghi
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns>số lượng thêm mới</returns>
        /// Created by: ttanh (30/06/2023)
        public virtual async Task<int> InsertAsync(TEntityCreatedDto entityDto)
        {
          

            var entity = _mapper.Map<TEntity>(entityDto);


            var properties = entity.GetType().GetProperties();


            foreach (var property in properties)
            {   
                var name = property.Name;
                if (name == $"{_tableName}Id")
                {
                    property.SetValue(entity, Guid.NewGuid());
                }
                else if (name == $"CreatedDate")
                {
                    property.SetValue(entity, DateTime.Now);
                }
                else if (name == $"CreatedBy")
                {
                    property.SetValue(entity, null);
                }
            }

            int result = await _baseRepository.InsertAsync(entity);

            return result;
        }

        /// <summary>
        /// cập nhật bản ghi
        /// </summary>
        /// <param name="entityDto"></param>
        /// <param name="id"></param>
        /// <returns>số lượng bản ghi cập nhật</returns>
        /// Created by: ttanh (30/06/2023)
        public virtual async Task<int> UpdateAsync(TEntityUpdatedDto entityDto, Guid id)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var name = property.Name;



                if (name == $"ModifiedDate")
                {
                    property.SetValue(entity, DateTime.Now);
                }
                else if (name == $"ModifiedBy")
                {
                    property.SetValue(entity, null);
                }
            }

            int result = await _baseRepository.UpdateAsync(entity, id);

            return result;
        }


        public virtual async Task<bool> IsValidEntity(TEntityCreatedDto entity)
        {

                var codeField = entity.GetType().GetProperty($"{_tableName}Code").Name;

                if (codeField != null)
                {
                    var code = entity.GetType().GetProperty(codeField).GetValue(entity, null);
                
                var dbEntity = await _baseRepository.GetByCodeAsync(code.ToString());

                    if (dbEntity != null)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }
        

        #endregion

    }
}
