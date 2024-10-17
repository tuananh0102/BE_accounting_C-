using AutoMapper.Configuration.Annotations;
using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Configuration;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using MISA.Web04.Infrastructure.UnitOfWork;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly IUnitOfWork _uow;
        private string tableName = typeof(TEntity).Name;


        #region Constructor
        public BaseRepository(IUnitOfWork uow)
        {
            _uow = uow;

        }
        #endregion

        #region Methods


        public virtual async Task InsertListAsync(IEnumerable<TEntity> listEntity)
        {


            var dynamicParams = new DynamicParameters();

            var sql = "";

            var index = 0;
            var snakeCaseTableName = ToSnakeCase(tableName);
            // tạo lệnh sql và add dynamic param
            foreach (var entity in listEntity)
            {
                var notNullProps = entity.GetType().GetProperties().Where(prop => prop.GetValue(entity) != null);
                sql += $"INSERT INTO {snakeCaseTableName} (";
                sql += string.Join(", ", notNullProps.Select(prop => prop.Name));
                sql += ") Values (";
                sql += string.Join(", ", notNullProps.Select(prop => $"@{prop.Name}_{index}"));
                sql += ");";

                foreach (var prop in notNullProps)
                {
                    dynamicParams.Add($"{prop.Name}_{index}", prop.GetValue(entity));
                }

                dynamicParams.Add($"{tableName}Id_{index}", Guid.NewGuid());
                index++;
            }


            await _uow.Connection.ExecuteAsync(sql, dynamicParams, transaction: _uow.Transaction);

        }


       



        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="id">id bàn ghi</param>
        /// <returns>số lượng bị xóa</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<int> DeleteAsync(Guid id)
        {


            var parameters = new DynamicParameters();
            parameters.Add($"@{tableName}Id", id);

            int result = await _uow.Connection.ExecuteAsync($"Proc_{tableName}_Delete", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            return result;


        }


        protected static string ConvertIdListToString(List<Guid> idList)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Guid id in idList)
            {
                stringBuilder.Append(id.ToString());
                stringBuilder.Append(",");
            }

            // Xóa dấu phẩy cuối cùng nếu có
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Length--;
            }

            return stringBuilder.ToString();
        }



        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">danh sách bản ghi</param>
        /// <returns>số lượng bản ghi bị xóa</returns>
        /// Created by: ttanh (30/06/2023)
        public virtual async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {


            var parameters = new DynamicParameters();
            string id_list = ConvertIdListToString(ids);
            parameters.Add("id_list", id_list);
            //string sql = $"DELETE FROM {tableName} WHERE {tableName}Id IN @ids";
            var result = await _uow.Connection.ExecuteAsync($"Proc_{tableName}_DeleteMultiple", parameters, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);



            return result;

        }

        /// <summary>
        /// lấy toàn bộ bản ghi 
        /// </summary>
        /// <returns>danh sách bản ghi</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {

            var parameters = new DynamicParameters();

            IEnumerable<TEntity> entities = await _uow.Connection.QueryAsync<TEntity>($"Proc_{tableName}_GetAll", null, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return entities;

        }

        /// <summary>
        /// lấy bản ghi theo id
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>thực thể</returns>
        /// Created by: ttanh (30/06/2023)
        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {


            var paramteters = new DynamicParameters();
            paramteters.Add($"@{tableName.ToLower()}Id", id);
            TEntity entity = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>($"Proc_{tableName}_GetById", paramteters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return entity;

        }


        public async Task<TEntity> GetByCodeAsync(string code)
        {

            var paramteters = new DynamicParameters();

            paramteters.Add($"@{tableName.ToLower()}Code", code);

            TEntity entity = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>($"Proc_{tableName}_GetByCode", paramteters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return entity;


        }


        /// <summary>
        /// thêm mới bản ghi
        /// </summary>
        /// <param name="entity">thực thể</param>
        /// <returns>số lượng bản ghi thêm</returns>
        /// Created by: ttanh (30/06/2023)
        public virtual async Task<int> InsertAsync(TEntity entity)
        {


            var parameters = new DynamicParameters();

            // map property của employee với tham số truyền vào database

            foreach (var prop in entity.GetType().GetProperties())
            {

                parameters.Add("@" + prop.Name, prop.GetValue(entity));
            }

            int result = await _uow.Connection.ExecuteAsync($"Proc_{tableName}_Insert", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            //int result = _uow.Connection.Execute(queryString, employee);
            return result;

        }


        /// <summary>
        /// cập nhật bản ghi
        /// </summary>
        /// <param name="entity">thực thể</param>
        /// <param name="id">id bản ghi</param>
        /// <returns>số lượng bản ghi</returns>
        /// Created by: ttanh (30/06/2023)
        public async Task<int> UpdateAsync(TEntity entity, Guid id)
        {

            var parameters = new DynamicParameters();


            // map property của employee với tham số truyền vào database

            foreach (var prop in entity.GetType().GetProperties())
            {

                parameters.Add("@" + prop.Name, prop.GetValue(entity, null));
            }
            parameters.Add($"@{tableName}Id", id);


            int result = await _uow.Connection.ExecuteAsync($"Proc_{tableName}_Update", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return result;

        }

        public static string ToSnakeCase(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public virtual async Task UpdateListAsync(IEnumerable<TEntity> listEntity)
        {
            var dynamicParams = new DynamicParameters();

            var sql = "";

            var index = 0;
            var snakeCaseTableName = ToSnakeCase(tableName);

            // Tạo lệnh SQL và add dynamic param
            foreach (var entity in listEntity)
            {
                var notNullProps = entity.GetType().GetProperties().Where(prop => prop.GetValue(entity) != null);
                sql += $"UPDATE {snakeCaseTableName} SET ";

                // Thêm các cột và giá trị tương ứng vào lệnh UPDATE
                var setStatements = notNullProps.Where(p => p.Name != $"{tableName}Id").Select(prop => $"{prop.Name} = @{prop.Name}_{index}");
                sql += string.Join(", ", setStatements);

                sql += $" WHERE {tableName}Id = @{tableName}Id_{index};";

                foreach (var prop in notNullProps)
                {
                    dynamicParams.Add($"{prop.Name}_{index}", prop.GetValue(entity));
                }
                index++;
            }

            await _uow.Connection.ExecuteAsync(sql, dynamicParams, transaction: _uow.Transaction);
        }

        public async Task<IEnumerable<TEntity>> GetListByKeySearchAsync(string? querySearch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@querySearch", querySearch);
            var entities = await _uow.Connection.QueryAsync<TEntity>($"Proc_{tableName}_GetListByKey", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return entities;
        }




        #endregion
    }
}
