namespace AdMoney.Models
{
    public class QuestionOptionsData
    {
        public int? QuestionId { get; set; } = 0;
        public string? Question { get; set; }

        public List<int>? OptionId { get; set; }
        public List<string>? Options { get; set; } 
    }
}
