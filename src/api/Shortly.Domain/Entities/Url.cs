using Shortly.Domain.Abstractions;
using Shortly.Domain.Abstractions.IEntities;
using Shortly.Domain.Entities.Identity;
using Shortly.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Domain.Entities
{
    [Table("Url")]
    public class Url : EntityBase<Guid>, IAuditBase
    {
        // Key properties
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid(); // Add an ID as the primary key (best practice).

        public string OriginalUrl { get; set; } = null!;

        public string? ShortenUrl { get; set; }

        // Metadata
        public bool IsActive { get; set; } = false; // Indicates if the URL is enabled or disabled.

        public UrlPrivacyLevel PrivacyLevel { get; set; } = UrlPrivacyLevel.Public; // Replaces IsPrivate for clarity.

        // Analytics
        public int AccessCount { get; set; } = 0; // Tracks access count.

        public DateTimeOffset? ExpirationDate { get; set; } // Optional expiration.

        // Description and comments
        public string? Description { get; set; }

        // Audit info
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? LastModifiedDate { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        // Navigation Property
        public ApplicationUser? User { get; set; } // Optional user relationship.


    }
}
