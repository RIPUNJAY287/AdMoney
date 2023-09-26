using System.ComponentModel.DataAnnotations.Schema;

namespace AdMoney.Models
{
    public class QuestionOption
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public  int OptionId { get; set; }
        
        public string OptionName { get; set; }

        public int Weight { get; set; }

        [ForeignKey("QuestionId")]
        public Question question { get; set; }
    }
}
