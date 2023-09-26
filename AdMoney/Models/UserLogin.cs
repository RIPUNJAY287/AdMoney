using Microsoft.AspNetCore.Identity;

namespace AdMoney.Models
{
    public class UserLogin
    {
        public string? email { get; set; }
        public string? password { get; set; }

        public string? role { get; set; }   
    }
}
