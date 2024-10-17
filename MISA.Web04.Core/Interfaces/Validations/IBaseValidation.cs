using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Validations
{
    public interface IBaseValidation<TEntity>
    {
        /// <summary>
        /// kiếm tra trùng mã
        /// </summary>
        /// <param name="code">mã</param>
        /// <param name="id">id bản ghi</param>
        /// <returns>throw exception nếu trùng mã</returns>
        /// Created by: ttanh(17/08/2023)
        Task CheckDuplicatedCodeAsync(string code, Guid? id);

        /// <summary>
        /// kiêm tra sự tồn tại của bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>throw exception nếu không tồn tại</returns>
        /// Created by: ttanh(17/08/2023)
        Task CheckExistIdAsync(Guid id);
    }
}
