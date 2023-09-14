using System.ComponentModel.DataAnnotations;

namespace AdMoney.Models
{
    public class UserSignup
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = null!;

        [Required]
        public string? Role { get; set; }
    }
}
