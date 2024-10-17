using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork:IDisposable, IAsyncDisposable
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }

      
        void BeginTransaction();
        Task BeginTransactionAsync();

        /// <summary>
        /// 
        /// </summary>
        void Commit();
        Task CommitAsync();

        void Rollback();
        Task RollbackAsync();

    }
}
