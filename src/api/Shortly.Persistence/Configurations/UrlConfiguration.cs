using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shortly.Domain.Entities;
using Shortly.Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Persistence.Configurations
{
    internal class UrlConfiguration : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.ToTable(TableName.Url);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OriginalUrl)
                .IsRequired(true)
                .HasMaxLength(2048);

            builder.Property(x => x.ShortenUrl)
               .IsRequired(false)
               .HasMaxLength(16);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(false);

            builder.Property(x => x.ExpirationDate);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.PrivacyLevel)
                .IsRequired()
               .HasConversion<int>();

            builder.Property(x => x.CreatedDate);

            builder.Property(x => x.LastModifiedDate);

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(x => x.LastModifiedDate);
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.ModifiedBy);
            builder.Property(x => x.AccessCount);


            #region Constraint
            
            builder.HasOne(su => su.User) // Each ShortenedUrl has one ApplicationUser
            .WithMany(u => u.ShortenUrls) // One ApplicationUser has many ShortenedUrls
            .HasForeignKey(su => su.CreatedBy) // The foreign key is "CreatedBy" in ShortenedUrl
            .OnDelete(DeleteBehavior.Restrict) // Prevent cascade deletes.
            .HasPrincipalKey(u => u.Id); // It maps to the "Id" field in ApplicationUser

            #endregion

            #region Index
            builder.HasIndex(su => su.ShortenUrl)
               .IsUnique()
               .HasDatabaseName("IX_Url_ShortenUrl");

            builder.HasIndex(su => su.CreatedBy)
                   .HasDatabaseName("IX_Url_CreatedBy");

            builder.HasIndex(su => su.OriginalUrl)
                   .HasDatabaseName("IX_Url_OriginalUrl");

            builder.HasIndex(su => su.CreatedDate)
                   .HasDatabaseName("IX_Url_CreatedDate");

            builder.HasIndex(su => new { su.OriginalUrl, su.CreatedBy })
                   .IsUnique()
                   .HasDatabaseName("IX_Url_OriginalUrl_CreatedBy");
            #endregion

            #region Query Filter
            builder.HasQueryFilter(x => x.IsActive); // Automatically filter by active status
            #endregion
        }
    }
}
