using Microsoft.EntityFrameworkCore;
using Shortly.Contract.DependencyInjections.Repositories;
using Shortly.Domain.Entities;
using Shortly.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Persistence.Dependencies.Repositories
{
    public class UrlRepository : RepositoryBase<Url, Guid>, IUrlRepository
    {
        public UrlRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Url?> GetByShortUrlAsync(string shortUrl)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.ShortenUrl == shortUrl);
        }
    }
}
