using Microsoft.AspNetCore.Identity;
using Shortly.Domain.Abstractions.IEntities;
using Shortly.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shortly.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, IAuditBase
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        #region Contact

        public string? MobileNumber { get; set; }

        public string? Fax { get; set; }

        #endregion

        #region Address
        public string? Address { get; set; }

        public string? Region { get; set; }

        public string? Country { get; set; }

        public string? Province { get; set; }

        public string? Postcode { get; set; }

        public string? StateCode { get; set; }

        #endregion

        public UserStatus UserStatus { get; set; }

        public AccountStatus AccountStatus { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? LastModifiedDate { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<Guid>> UserLogins { get; set; }

        public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }

        public virtual ICollection<IdentityUserRole<Guid>> Roles { get; set; }

        public virtual ICollection<Url> ShortenUrls { get; set; }
    }
}
