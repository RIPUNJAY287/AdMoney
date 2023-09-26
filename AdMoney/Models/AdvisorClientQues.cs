using System.ComponentModel.DataAnnotations;

namespace AdMoney.Models
{
    public class AdvisorClientQues
    {
        [Key]
        public int Id { get; set; }

        public int AdvisorId { get; set; }

        public int ClientId { get; set; }
        
        public int QuestionId { get; set; }

        public int OptionId { get; set; }



    }
}
