using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Application.Dependencies.DataAccess
{
    public interface IUnitOfwork
    {
        bool HasActiveTransaction { get; }

        Task SaveChangeAsync(CancellationToken cancellationToken = default);

        DbContext GetDbContext();

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
