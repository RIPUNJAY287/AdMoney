using System.ComponentModel.DataAnnotations;

namespace AdMoney.Models
{
    public class AddClientData
    {

        public string? Name { get; set; }

        public string? Email { get; set; }

     
        public string? PanCard { get; set; }

        public string? AadharCard { get; set; }
    }
}
