using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Contract.Services.V1.Identity
{

    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max length is 50 characters")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

    }
}
