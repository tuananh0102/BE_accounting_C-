using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IReceiptRepository : IBaseRepository<Receipt>
    {
        /// <summary>
        /// lọc danh sách dữ liệu
        /// </summary>
        /// <param name="pageSize">số bản ghi trên một trang</param>
        /// <param name="pageIndex">trang thứ mấy</param>
        /// <param name="querySearch">tư khóa tìm kiếm</param>
        /// <returns></returns> 
        Task<(int, IEnumerable<Receipt>)> GetFilter(int pageSize, int pageIndex, string? querySearch, bool? type);

        /// <summary>
        /// lấy mã code lớn nhất
        /// </summary>
        /// <returns>mã code lớn nhất</returns>
        /// Created by: ttanh (07/08/2023)
        Task<string> GetMaxCode();
        /// <summary>
        /// lấy tập id theo list id
        /// </summary>
        /// <param name="listId">list id tìm kiếm</param>
        /// <param name="isNoted">trạng thái ghi sổ hay không</param>
        /// <returns>list id</returns>
        Task<IEnumerable<Guid>> GetByListId(List<Guid> listId, bool isNoted);

        /// <summary>
        /// lấy bản ghi theo tìm kiếm
        /// </summary>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <returns>list bản ghi</returns>
        //Task<IEnumerable<Receipt>> GetListByKeySearchAsync(string? querySearch);

    }
}
