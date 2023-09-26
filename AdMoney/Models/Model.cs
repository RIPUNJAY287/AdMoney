using Microsoft.AspNetCore.Hosting.StaticWebAssets;

namespace AdMoney.Models
{
    public class Model
    {
        public int Id { get; set; }
        
        public int modelId { get; set; }

        public int userId { get; set; }
        public string? asset { get; set; }
        public string? security { get; set; }

    }
}
