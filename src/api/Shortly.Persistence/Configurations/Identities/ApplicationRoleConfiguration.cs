using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shortly.Domain.Entities.Identity;
using Shortly.Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Persistence.Configurations.Identities
{
    internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable(TableName.ApplicationRole);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).IsRequired(true);
            builder.Property(x => x.RoleCode).IsRequired(true);

            // Each user has many RoleClaim
            builder.HasMany(t => t.Claims)
                .WithOne()
                .HasForeignKey(t => t.RoleId)
                .IsRequired();

            // Each user has many UserRole
            builder.HasMany(t => t.Roles)
                .WithOne()
                .HasForeignKey(t => t.RoleId)
                .IsRequired();
        }
    }
}
