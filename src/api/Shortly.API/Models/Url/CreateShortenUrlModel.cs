using System.ComponentModel.DataAnnotations;

namespace Shortly.API.Models.Url
{
    public class CreateShortenUrlModel
    {
        [Required]
        [StringLength(100)]
        public string OriginalUrl { get; set; }

        public string? CustomKey { get; set; }
    }
}
