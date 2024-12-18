using Shortly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Contract.DependencyInjections.Repositories
{
    public interface IUrlRepository : IRepositoryBase<Url, Guid>
    {
        Task<Url?> GetByShortUrlAsync(string shortUrl);
    }
}
