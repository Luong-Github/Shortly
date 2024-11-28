using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }

        public string? RoleCode { get; set; }

        public virtual ICollection<IdentityRoleClaim<Guid>> Claims { get; set; }

        public virtual ICollection<IdentityUserRole<Guid>> Roles { get; set; }
    }
}
