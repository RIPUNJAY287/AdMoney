using System.ComponentModel.DataAnnotations;

namespace AdMoney.Models
{
    public class AdminUser
    {
        [Key]    
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }

    }
}
