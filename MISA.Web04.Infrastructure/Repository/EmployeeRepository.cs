using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web04.Core.Dto;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        public EmployeeRepository(IUnitOfWork uow) : base(uow)
        {

        }
        #endregion

        #region Methods

        /// <summary>
        /// lấy danh sách nhân viên
        /// </summary>
        /// <param name="querySearch">từ khóa tìm kiếm</param>
        /// <param name="recordsPerPage">số bản ghi 1 trang</param>
        /// <param name="page">trang thứ mấy</param>
        /// <returns>danh sách nhân viên</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<(int, IEnumerable<Employee>)> GetListAsync(string? querySearch, int? recordsPerPage, int? page)
        {

            
                var parameters = new DynamicParameters();
                parameters.Add("@querySearch", querySearch);
                parameters.Add("@recordsPerPage", recordsPerPage);
                parameters.Add("@pageOffset", recordsPerPage * (page - 1));
                parameters.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);


                var employees = await _uow.Connection.QueryAsync<Employee>("Proc_Employee_Filter", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
                int totalRecord = parameters.Get<int>("totalRecord");
                return (totalRecord, employees);
            
        }


        /// <summary>
        /// kiểm tra mã trùng
        /// </summary>
        /// <param name="code">mã cần kiểm tra</param>
        /// <returns>true or false</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<bool> IsDuplicateCodeAsync(string code)
        {
            
                var employee = await base.GetByCodeAsync(code);
                if (employee != null) return true;
                return false;


            
        }

        /// <summary>
        /// kiểm tra id có tồn tại không
        /// </summary>
        /// <param name="id">id cần kiểm tra</param>
        /// <returns>true or false</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<bool> IsExistedIdAsync(Guid id)
        {
            
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", id);
                Employee employee = await _uow.Connection.QueryFirstOrDefaultAsync<Employee>("Proc_Employee_GetById", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
                if (employee != null) return true;
                return false;
            
        }

        /// <summary>
        /// lấy ra mã lớn nhất
        /// </summary>
        /// <returns>mã lớn nhất</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<string> GetMaxCode()
        {
            
                string maxCode = await _uow.Connection.QueryFirstOrDefaultAsync<string>("Proc_Employee_GetMaxCode", null, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
                return maxCode;
            
        } 
        #endregion
    }

}
