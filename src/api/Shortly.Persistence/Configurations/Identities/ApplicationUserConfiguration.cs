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
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(TableName.ApplicationUser);

            builder.HasKey(t => t.Id);
            builder
                .Property(t => t.FirstName)
                .IsRequired(true);
            builder
                .Property(t => t.LastName)
                .IsRequired(true);
            builder.Property(t => t.DateOfBirth);

            #region Contact

            builder.Property(t => t.MobileNumber);
            builder.Property(t => t.Fax);

            #endregion

            #region Address
            builder.Property(t => t.Address);
            builder.Property(t => t.Region);
            builder.Property(t => t.Country);
            builder.Property(t => t.Province);
            builder.Property(t => t.Postcode);
            builder.Property(t => t.StateCode);

            #endregion

            builder.Property(t => t.UserStatus);
            builder.Property(t => t.AccountStatus);
            builder.Property(t => t.CreatedDate);
            builder.Property(t => t.LastModifiedDate);
            builder.Property(t => t.CreatedBy);
            builder.Property(t => t.ModifiedBy);

            #region Constraint
            builder.HasMany(t => t.Claims)
                .WithOne()
                .HasForeignKey(t => t.UserId)
                .IsRequired();

            builder.HasMany(t => t.UserLogins)
                .WithOne()
                .HasForeignKey(t => t.UserId)
                .IsRequired();

            builder.HasMany(t => t.Tokens)
                .WithOne()
                .HasForeignKey(t => t.UserId)
                .IsRequired();

            builder.HasMany(t => t.Roles)
                .WithOne()
                .HasForeignKey(t => t.UserId)
                .IsRequired();
            #endregion

            #region Index
            builder.HasIndex(x => x.Id)
                .IsUnique();
            builder.HasIndex(x => x.Email)
                .IsUnique();
            #endregion

            #region QueryFilter for all

            #endregion
        }
    }
}
