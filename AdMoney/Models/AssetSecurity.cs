using System.ComponentModel.DataAnnotations;

namespace AdMoney.Models
{
    public class AssetSecurity
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Asset { get; set; }

        [MaxLength(200)]
        public string SecurityName { get; set; }

    }
}
