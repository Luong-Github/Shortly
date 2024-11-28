using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Application.Dependencies.DataAccess
{
    public interface IUnitOfWorkDbContext<TContext> : IAsyncDisposable
        where TContext : DbContext
    {
        Task SaveChangeAsync(CancellationToken cancellationToken = default);

        DbContext GetDbContext();
    }
}
