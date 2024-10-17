using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.UnitTests.Core
{
    public class FakeDepartmentRepository : IDepartmentRepository
    {
        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetListAsync(string queryName)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Department entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Department entity, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
