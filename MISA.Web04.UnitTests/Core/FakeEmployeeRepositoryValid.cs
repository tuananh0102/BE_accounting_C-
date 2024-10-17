using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Resources.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.UnitTests.Core
{
    public class FakeEmployeeRepositoryValid : IEmployeeRepository
    {
        public Guid ActualId { get; set; }
        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
      

        public Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByIdAsync(Guid id)
        {
            ActualId = id;
            return Task.FromResult(new Employee()
            {
                EmployeeId = id,
            });
        }

        public Task<(int, IEnumerable<Employee>)> GetListAsync(string? queryName, int? recordsPerPage, int? page)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetMaxCode()
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDuplicateCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistedIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Employee entity, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
