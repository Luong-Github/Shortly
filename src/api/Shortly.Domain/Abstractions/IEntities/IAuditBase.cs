using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Domain.Abstractions.IEntities
{
    public interface IAuditBase : IDateTracking, IUserTracking
    {
    }
}
