using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shortly.Contract.Dependencies.DataAccess;
using Shortly.Contract.DependencyInjections.Repositories;
using Shortly.Persistence.Contexts;
using Shortly.Persistence.Dependencies.Repositories;

namespace Shortly.Infrastructure.Abstractions.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _dbContextTransaction;

        private readonly Lazy<UrlRepository> _urlRepository;

        #endregion

        #region Constructor

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _urlRepository = new Lazy<UrlRepository>(() => new UrlRepository(_context));
        }

        #endregion

        #region Properties

        public IUrlRepository UrlRepository => _urlRepository.Value;
        public bool HasActiveTransaction => _dbContextTransaction is not null;

        #endregion

        #region Transaction Management

        public async Task BeginTransactionAsync()
        {
            if (_dbContextTransaction is not null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _dbContextTransaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);
        }

        public async Task CommitTransactionAsync()
        {
            if (_dbContextTransaction == null)
            {
                throw new InvalidOperationException("No active transaction to commit.");
            }

            try
            {
                await SaveChangeAsync();
                await _dbContextTransaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_dbContextTransaction == null)
            {
                throw new InvalidOperationException("No active transaction to rollback.");
            }

            try
            {
                await _dbContextTransaction.RollbackAsync();
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        private async Task DisposeTransactionAsync()
        {
            if (_dbContextTransaction is not null)
            {
                await _dbContextTransaction.DisposeAsync();
                _dbContextTransaction = null;
            }
        }

        #endregion

        #region SaveChanges

        public async Task SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Cleanup

        public async ValueTask DisposeAsync()
        {
            if (_dbContextTransaction != null)
            {
                await _dbContextTransaction.DisposeAsync();
            }

            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        public DbContext GetDbContext()
        {
            return _context;
        }

        #endregion
    }
}
