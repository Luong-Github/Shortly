using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shortly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Persistence.Configurations
{
    internal class TokenInforConfiguration : IEntityTypeConfiguration<TokenInfo>
    {
        public void Configure(EntityTypeBuilder<TokenInfo> builder)
        {
            builder.ToTable(nameof(TokenInfo));

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Username)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.RefreshToken)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ExpiredAt)
                .IsRequired();


            // constraint

            
            // has query filter
        }
    }
}
