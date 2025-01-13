﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Contract.Abstractions
{
    public interface ICacheService
    {
        T GetData<T>(string key);

        bool SetData<T>(string key, T data, DateTimeOffset expirationTime);

        object RemoveData<T>(string key);
    }
}
