using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IProviderRepository : IBaseRepository<Provider>
    {
        Task<(int,IEnumerable<Provider>)> GetFilter(int pageSize, int pageIndex, string? querySearch);
        //Task<int> InsertRelationshipAsync(Provider provider);
        //Task<int> DeleteRelationshipAsync(Guid providerId);

        //Task<int> UpdateRelationshipAsync(Provider provider, Guid providerId,List<Guid> oldIdList, List<Guid> newIdList);

        /// <summary>
        /// lấy mã code lớn nhất
        /// </summary>
        /// <returns>mã code lớn nhất</returns>
        /// Created by: ttanh (09/08/2023)
        Task<string> GetMaxCode();

    }
}
