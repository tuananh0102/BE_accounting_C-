using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructor
        public DepartmentRepository(IUnitOfWork uow) : base(uow)
        {

        }
        #endregion

        /// <summary>
        /// lấy danh sách đơn vị
        /// </summary>
        /// <param name="queryName">từ khóa tìm kiếm</param>
        /// <returns>danh sách đơn vị</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<IEnumerable<Department>> GetListAsync(string queryName)
        {
            
                var parameters = new DynamicParameters();
                parameters.Add("@departmentName", queryName);
                IEnumerable<Department> departments = await _uow.Connection.QueryAsync<Department>("Proc_Department_GetAll", parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);
                return departments;
            
        }
    }
}
