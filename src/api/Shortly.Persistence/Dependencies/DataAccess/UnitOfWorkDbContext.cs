using Microsoft.EntityFrameworkCore;
using Shortly.Contract.Dependencies.DataAccess;

namespace Shortly.Infrastructure.Abstractions.DataAccess
{
    public class UnitOfWorkDbContext<TDbContext> : IUnitOfWorkDbContext<TDbContext>
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        public UnitOfWorkDbContext(TDbContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public DbContext GetDbContext()
        {
            return _context;
        }

        public async Task SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync();
        }
    }
}
