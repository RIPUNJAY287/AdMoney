using System.ComponentModel.DataAnnotations;

namespace AdMoney.Models
{
    public class Client
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]        
        public string? Name { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string? PanCard { get; set; }

        [Required]
        [MaxLength(20)]
        public string? AadharCard { get; set; }
 
        public int? AdvisorId { get; set; }

        [MaxLength(100)]
        public string? RiskProfile { get; set; }

        public int? modelId { get; set; }   
    }
}
