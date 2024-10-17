using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Validations
{
    public interface IAccountValidation : IBaseValidation<Account>
    {
        /// <summary>
        /// kiểm tra tài khoản cha
        /// </summary>
        /// <param name="accountId">id tài khoản cha</param>
        /// <returns>throw ra exception nếu không thỏa mãn</returns>
        Task CheckValidParentAsync(Guid accountId);

        /// <summary>
        /// kiểm tra cha của tài khoản có đang ngừng sử dụng không
        /// </summary>
        /// <param name="code">mã tài khoản</param>
        /// <returns>throw exception nếu tài khoản cha đang ngừng sử dụng</returns>
        Task CheckValidStatusToUseAsync(string code);

        /// <summary>
        /// kiểm tra mã code có hợp lệ không
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task CheckValidCodeAsync(string code, Guid? id, Guid parentId);

        /// <summary>
        /// kiểm tra có xóa được không
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task CheckDeleteAsync(Guid accountId);
    }
}
