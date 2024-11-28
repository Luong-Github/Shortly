using Shortly.Domain.Abstractions;
using Shortly.Domain.Abstractions.IEntities;
using Shortly.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Domain.Entities
{
    public class Url : EntityBase<Guid>, IAuditBase
    {
        [Required]
        public string OriginalUrl { get; set; }

        public string? ShortenUrl { get; set; }

        public bool IsActive { get; set; } = false;

        public bool IsPrivate { get; set; }

        public int AccessCount { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }

        public string? Description { get; set; }

        //public UrlPrivacyLevel UrlPrivacyLevel { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? LastModifiedDate { get; set; }

        public Guid? ModifiedBy { get; set; }

        public Guid CreatedBy { get; set; }

        public ApplicationUser? User { get; set; }


    }
}
