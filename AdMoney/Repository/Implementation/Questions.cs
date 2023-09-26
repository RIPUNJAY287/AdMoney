using AdMoney.Data;
using AdMoney.Models;
using AdMoney.Repository.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;

namespace AdMoney.Repository.Implementation
{
    public class Questions : IQuestions
    {
        private readonly AdMoneyContext _context;
        public Questions(AdMoneyContext context) { _context = context; }

        public void AddAdvisorQuesAns(AdvisorClientQues advisorClientQues)
        {
            _context.AdvisorClientsQues.Add(advisorClientQues);
            _context.SaveChanges();
        }

        public string GetRiskProfile( int clientId)
        {
               var sumAnswer =   (from adClient in _context.AdvisorClientsQues
                                 join quesOpt in _context.QuestionOptions
                                 on adClient.QuestionId equals quesOpt.QuestionId 
                                 where  adClient.OptionId == quesOpt.OptionId && adClient.ClientId == clientId
                                 select quesOpt.Weight).Sum();
            Console.WriteLine(sumAnswer) ;
            var riskData = _context.RiskProfiles.ToList();
            int lowRisk = 0;
            int midRisk = 0;
            int highRisk = 0;
            foreach( var wt in riskData )
            {
                if (lowRisk == 0)
                {
                    lowRisk = wt.Thresold;
                }
                else if (lowRisk == 0)
                {
                    midRisk = wt.Thresold;
                }
                else
                {
                    highRisk = wt.Thresold;
                }

            }
            return sumAnswer <= lowRisk ? "LowRisk" : (sumAnswer <= midRisk ? "MidRisk" : "HighRisk");
        }

        public List<QuestionOptionsData> GetAllQuestionOptions()
        {
            var questionOptions = from ques in _context.Questions
                                  from quesopt in _context.QuestionOptions
                                  where ques.Id == quesopt.QuestionId
                                  select new
                                  {
                                      questionId = ques.Id, 
                                      question = ques.QuestionName,
                                      optionId = quesopt.OptionId,
                                      optionsName = quesopt.OptionName
                                  };

            int quesId = 0;
            List<QuestionOptionsData> queOptionsList = new List<QuestionOptionsData>();
            QuestionOptionsData? questionOptionData = new QuestionOptionsData();
            List<string>? optionList = new List<string>();
            foreach ( var obj in questionOptions)
            {
                Console.WriteLine(obj.questionId + " " + obj.question+ " " +  obj.optionsName +  " " + quesId);
                if (quesId != obj.questionId)
                {
                    if (quesId != 0)
                    {
                        questionOptionData.Options = optionList;
                        queOptionsList.Add(questionOptionData);
                    }
                    quesId = obj.questionId;
                    questionOptionData = new QuestionOptionsData();
                    optionList = new List<string>();
                    questionOptionData.Question = obj.question;
                }
                optionList.Add(obj.optionsName);
                
            }
            if(quesId != 0)
            {
                questionOptionData.Options = optionList;
                queOptionsList.Add(questionOptionData);
            }
            return queOptionsList;
        }

        public QuestionOptionsData GetQuestionOptions(int clientId , int advisorId)
        {
            Console.WriteLine(clientId + "    " + advisorId);
            var quesObj = from adques in _context.AdvisorClientsQues 
                              where adques.ClientId == clientId  && adques.AdvisorId == advisorId
                              select new { questionId = adques.QuestionId };
            
            List<int> quesList = new List<int>();

            foreach (var Id in quesObj)
            {
                Console.WriteLine(Id.questionId);

                quesList.Add(Id.questionId);
            }

            var questionObj = from ques in _context.Questions
                     join quesopt in _context.QuestionOptions
                     on ques.Id equals quesopt.QuestionId
                     where  !quesList.Contains(ques.Id)
                    select new
                    {
                        questionId = ques.Id, 
                        question = ques.QuestionName,
                        optionId = quesopt.OptionId,
                        optionsName = quesopt.OptionName
                    };

            int quesId = 0;
            if (questionObj.Any())
            {
                QuestionOptionsData questionOptionData = new QuestionOptionsData();
                List<string> optionList = new List<string>();
                List<int> optionIdList = new List<int>();
                bool flag = false;
                foreach (var obj in questionObj)
                {
                    Console.WriteLine(obj.questionId + " " + obj.question + " " + obj.optionsName );
                    if (quesId != obj.questionId)
                    {
                        if (quesId != 0)
                        {
                            questionOptionData.OptionId = optionIdList;
                            questionOptionData.Options = optionList;
                            flag = true;
                            break;
                        }
                        quesId = obj.questionId;
                        questionOptionData = new QuestionOptionsData();
                        optionList = new List<string>();
                        questionOptionData.Question = obj.question;
                        questionOptionData.QuestionId = quesId;
                    }
                    optionList.Add(obj.optionsName);
                    optionIdList.Add(obj.optionId);
                }
                if(!flag) {
                    questionOptionData.OptionId = optionIdList;
                    questionOptionData.Options = optionList;
                }
                return questionOptionData;
            }
            return null;
        }

        public void AddAdvisorToClient(int clientId,  int advisor, string risk)
        {
            Console.WriteLine(clientId + "  - -   " + advisor+ " -- - " + risk);
            var cli = _context.Clients.Where(c => c.Id == clientId).FirstOrDefault();
            if (cli != null)
            {
               // cli.AdvisorId = advisor;
                cli.RiskProfile = risk;
            }
            _context.SaveChanges();
        }
    }
}
