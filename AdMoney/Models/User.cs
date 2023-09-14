using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace AdMoney.Models
{
    public class User
    {
        public int Id { get; set; }
     
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
