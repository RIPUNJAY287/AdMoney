using AdMoney.Models;

namespace AdMoney.Repository.Interfaces
{
    public interface IQuestions 
    {
        public List<QuestionOptionsData> GetAllQuestionOptions();
        public QuestionOptionsData GetQuestionOptions(int ClientId, int AdvisorId);

        public void AddAdvisorQuesAns(AdvisorClientQues advisorClientQues);

        public string GetRiskProfile(int clientId);

        public void AddAdvisorToClient(int clientId , int advisorId, string riskProfile);

    }
}
