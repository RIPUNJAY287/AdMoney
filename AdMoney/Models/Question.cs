using System.ComponentModel.DataAnnotations.Schema;

namespace AdMoney.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }


        public ICollection<QuestionOption> questionOption { get; set; }

       

    }
}
