using Shortly.Contract.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Infrastructure.Caching
{
    public class CacheService : ICacheService
    {
        public T GetData<T>(string key)
        {
            throw new NotImplementedException();
        }

        public object RemoveData<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool SetData<T>(string key, T data, DateTimeOffset expirationTime)
        {
            throw new NotImplementedException();
        }
    }
}
