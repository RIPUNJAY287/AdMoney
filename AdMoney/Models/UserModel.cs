using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdMoney.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public int userId { get; set; }

        
        public int modelId { get; set; }

        [MaxLength(100)]
        public string? risk { get; set; }
    }
}
