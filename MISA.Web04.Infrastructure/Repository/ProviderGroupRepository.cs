using Dapper;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Repository
{
    public class ProviderGroupRepository : BaseRepository<GroupProvider>, IProviderGroupRepository
    {
        

        public ProviderGroupRepository(IUnitOfWork uow):base(uow) 
        {
            
        }

        public async Task<IEnumerable<GroupProvider>> GetListByIdAsync(Guid? providerId, Guid? groupId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProviderId", providerId);
            parameters.Add("@GroupId", groupId);

            var groupProviders = await _uow.Connection.QueryAsync<GroupProvider>("Proc_GroupProvider_GetByIds", parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            return groupProviders;
        }

        public virtual async Task UpdateListAsync(IEnumerable<GroupProvider> listEntity)
        {
            var dynamicParams = new DynamicParameters();
            var sql = "";

            var index = 0;


            // Tạo lệnh SQL và add dynamic params
            foreach (var entity in listEntity)
            {
                var notNullProps = entity.GetType().GetProperties().Where(prop => prop.GetValue(entity) != null);

                sql += $"UPDATE group_provider SET ";
                sql += string.Join(", ", notNullProps.Select(prop => $"{prop.Name} = @{prop.Name}_{index}"));
                sql += $" WHERE ProviderId = @group_provider_id_{index};";

                foreach (var prop in notNullProps)
                {
                    dynamicParams.Add($"{prop.Name}_{index}", prop.GetValue(entity));
                }

                dynamicParams.Add($"group_provider_id_{index}", entity.ProviderId); // Thay entity.Id bằng thuộc tính Id thực tế của đối tượng TEntity

                index++;
            }

            await _uow.Connection.ExecuteAsync(sql, dynamicParams, transaction: _uow.Transaction);
        }


    }
}
