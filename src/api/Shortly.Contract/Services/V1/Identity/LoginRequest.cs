using System.ComponentModel.DataAnnotations;

namespace Shortly.Contract.Services.V1.Identity
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } =  string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
