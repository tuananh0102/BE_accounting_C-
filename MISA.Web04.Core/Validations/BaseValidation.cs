using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Validations;
using MISA.Web04.Core.Resources.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Validations
{
    public class BaseValidation<TEntity> : IBaseValidation<TEntity>
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private string tableName = typeof(TEntity).Name;

        public BaseValidation(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task CheckDuplicatedCodeAsync(string code, Guid? id)
        {
            var entity = await _baseRepository.GetByCodeAsync(code);

            if (entity != null)
            {
                if (id == null)
                {
                    throw new ValidateException(new Dictionary<String, List<String>> { { $"{tableName}Code", new List<string> {string.Format(AccountVN.EXISTED_CODE, code)} } });
                }

                var entityId = entity.GetType().GetProperty($"{tableName}Id").GetValue(entity, null);

                if (id != null && new Guid(entityId.ToString()) != id)
                {
                    throw new ValidateException(new Dictionary<String, List<String>> { { $"{tableName}Code", new List<string> { string.Format(AccountVN.EXISTED_CODE, code) } } });
                }
            }


            



        }

        public async Task CheckExistIdAsync(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Không tồn tại");
            }
        }
    }
}
