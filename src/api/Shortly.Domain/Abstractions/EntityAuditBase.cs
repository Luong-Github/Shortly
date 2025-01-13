using Shortly.Domain.Abstractions.IEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Domain.Abstractions
{
    public abstract class EntityAuditBase<TKey> : EntityBase<TKey>, IEntityAuditBase<TKey>
    {
        public DateTimeOffset CreatedDate { get ; set ; }
        public Guid CreatedBy { get ; set ; }
        public bool IsDeleted { get ; set ; }
        public DateTimeOffset? DeletedAt { get ; set ; }
        public DateTimeOffset? LastModifiedDate { get ; set ; }
        public Guid? ModifiedBy { get ; set ; }
    }
}
