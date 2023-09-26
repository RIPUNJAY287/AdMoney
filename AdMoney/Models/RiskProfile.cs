using System.ComponentModel.DataAnnotations;

namespace AdMoney.Models
{
    public class RiskProfile
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Risk { get; set; }

        public int Thresold { get; set; }
    }
}
