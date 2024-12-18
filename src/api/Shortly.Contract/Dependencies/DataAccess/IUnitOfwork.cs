using Microsoft.EntityFrameworkCore;
using Shortly.Contract.DependencyInjections.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Contract.Dependencies.DataAccess
{
    public interface IUnitOfWork
    {
        #region Accessory
        IUrlRepository UrlRepository { get; }

        #endregion

        bool HasActiveTransaction { get; }

        Task SaveChangeAsync(CancellationToken cancellationToken = default);

        DbContext GetDbContext();

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
