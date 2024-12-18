using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Application.Features.V1.Urls.Dependencies
{
    public interface IUrlServices
    {
        Task<string> CreateShortenUrlAsync(string originalUrl);
    }
}
