using AdMoney.Models;
using AdMoney.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace AdMoney.Controllers
{
    public class AdvisorController : Controller
    {

        private readonly ILogger<AdvisorController> _logger;
        private readonly IAdvisorClientData _advisorClientData;
        private readonly IModels _models;
        private readonly IQuestions _question;

        public AdvisorController(ILogger<AdvisorController> logger,IAdvisorClientData advisorClientData, IModels models , IQuestions question)
        {
            _logger = logger;
            _advisorClientData = advisorClientData;
            _models = models;
            _question = question;
        }
      
        private User? GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            Console.WriteLine(identity);
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var Name = userClaims.FirstOrDefault(v => v.Type == ClaimTypes.Name)?.Value;
                var Email = userClaims.FirstOrDefault(v => v.Type == ClaimTypes.Email)?.Value;
                var Role = userClaims.FirstOrDefault(v => v.Type == ClaimTypes.Actor)?.Value;
                var Id = userClaims.FirstOrDefault(v => v.Type == ClaimTypes.Sid)?.Value;
                Console.WriteLine(Name + " " + Email + " " + Role);
                User usr = new User();
                usr.Name = Name;
                usr.Email = Email;
                usr.Role = Role;
                usr.Id = int.Parse(Id);
                return usr;

            }
            return null;
        }

        [Authorize(Roles = "Advisor")]
        public IActionResult AdvisorProfile()
        {
            User user = GetCurrentUser();
            if (user != null)
            {

                User advisorPro = _advisorClientData.GetAdvisorProfile(user.Id);
                List<ModelSelectData> lowrisk = _advisorClientData.GetAllModels("LowRisk", user.Id); 
                List<ModelSelectData> midRisk = _advisorClientData.GetAllModels("MidRisk", user.Id); 
                List<ModelSelectData> highrisk = _advisorClientData.GetAllModels("HighRisk", user.Id);
                List<Client> clis = _advisorClientData.getAllAddedClients(user.Id);
                ViewData["UserProfile"] = advisorPro;
                ViewData["LowRiskModel"] = lowrisk;
                ViewData["MidRiskModel"] = midRisk;
                ViewData["HighRiskModel"] = highrisk;
                ViewData["Client"] = clis;

            }
            return View();
        }


        [Authorize(Roles = "Advisor")]
        public IActionResult AddClient()
        {

            return View();
        }
        [Authorize(Roles = "Advisor")]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Advisor")]
        [HttpPost]
        public IActionResult AddClient(AddClientData client)
        {
            User user = GetCurrentUser();
            Console.WriteLine(client.Name + "  " + client.Email);
            if (user != null && client!=null)
            {
                Client c = new Client()
                {
                    Name = client.Name,
                    Email = client.Email,
                    PanCard = client.PanCard,
                    AadharCard = client.AadharCard,
                    AdvisorId = user.Id,
                    RiskProfile = null,
                    modelId = null
                };
                _advisorClientData.AddNewClient(c);

            }
            return RedirectToAction("AddClient", "Advisor");
        }

        [Authorize(Roles = "Advisor")]
        public IActionResult Question(int clientId)
        {
            User user = GetCurrentUser();
            if (user != null)
            {
                QuestionOptionsData qod = _question.GetQuestionOptions(clientId,user.Id);

                ViewData["QuestionForAdClient"]  = qod;
                ViewData["NextQuestion"] = qod != null ? true: false;
                ViewData["clientId"] = clientId;
                return View();
            }
            return RedirectToAction("AdvisorClients", "Advisor");
        }

        [Authorize(Roles = "Advisor")]
        [HttpPost]
        public IActionResult NextQuestion([FromBody] AdvisorClientQuesForm advisorClientQuesForm )
        {
            User user = GetCurrentUser();
            Console.WriteLine(user.Id);
            if (user != null)
            {



                if (advisorClientQuesForm.QuestionId != 0)
                {
                    AdvisorClientQues advisorClientQues = new AdvisorClientQues();
                    advisorClientQues.QuestionId = advisorClientQuesForm.QuestionId;
                    advisorClientQues.OptionId = advisorClientQuesForm.OptionId;
                    advisorClientQues.ClientId = advisorClientQuesForm.ClientId;
                    advisorClientQues.AdvisorId = user.Id;
                    Console.WriteLine(advisorClientQues.QuestionId + " -  " + advisorClientQues.OptionId + "  - " + advisorClientQues.ClientId + " - " + advisorClientQues.AdvisorId);
                    _question.AddAdvisorQuesAns(advisorClientQues);
                    QuestionOptionsData obj = _question.GetQuestionOptions(advisorClientQues.ClientId, user.Id);

                    if (obj != null)
                    {
                        Console.WriteLine(obj.QuestionId + " inside");
                        var questionData = new
                        {
                            QuestionForAdClient = obj,
                            NextQuestion = true
                        };
                        return Ok(JsonConvert.SerializeObject(questionData));
                    }
                    else
                    {
                        
                        var questionData = new
                        {
                            RiskProfile = _question.GetRiskProfile(advisorClientQuesForm.ClientId),
                            NextQuestion = false
                        };
                        Console.WriteLine(questionData.RiskProfile);
                        _question.AddAdvisorToClient(advisorClientQues.ClientId, user.Id, questionData.RiskProfile);


                        return Ok(JsonConvert.SerializeObject(questionData));
                    }
                }
                else
                {
                    var questionData = new
                    {
                        RiskProfile = _question.GetRiskProfile(advisorClientQuesForm.ClientId),
                        NextQuestion = false
                    };
                    Console.WriteLine(questionData.RiskProfile);
                 
                    return Ok(JsonConvert.SerializeObject(questionData));
                }
            }
            return BadRequest("Not authenticated user");
        }

        [Authorize(Roles = "Advisor")]
        public IActionResult RiskProfileModel(int clientId)
        {
            User user = GetCurrentUser();
            if(user != null)
            {
                Client client = _advisorClientData.GetClientById(clientId);
                Console.WriteLine(client.AadharCard  );
                if (client != null)
                {   
                    List<ModelSelectData> modelList =_advisorClientData.GetAllModels(client.RiskProfile,user.Id);
                    foreach ( var md in modelList )
                    {
                        Console.WriteLine(md.modelId +  " " + md.riskProfile + " ---> ");
                        if (md.assetInfo != null)
                        {
                            foreach (var k in md.assetInfo)
                            {
                                Console.WriteLine(k.assetId +  "   " + k.assetName + "  " + k.securityName + " - >>  " + k.weight);
                            }
                        }
                    }
                    ViewData["clientId"] = clientId;
                    ViewData["modelList"] = modelList;

                }
            }
            return View();
        }

        [Authorize(Roles = "Advisor")]
        public IActionResult AdvisorClients()   
        {
            User user = GetCurrentUser();
            if (user != null)
            {
                List<Client> clients = _advisorClientData.getAllClients(user.Id);
                ViewData["ClientList"] = clients;
            }
         
            return View();
        }

        [Authorize(Roles = "Advisor")]
        public IActionResult Models()
        {
            List<AssetSecurity> models = _models.GetAllAssetSecurity().ToList();
            ViewData["AssetSecurityList"] = models; 
            
            return View();

        }

        [Authorize(Roles = "Advisor")]
        [HttpPost]
        public IActionResult AddModel([FromBody] ModelInputForm modelInputForm)
        {
           Console.WriteLine("ehllooo "  + modelInputForm.modelInput.Count);
            

            User user = GetCurrentUser();

            if (user != null)
            {
                int wt = 0;
                foreach (var item in modelInputForm.modelInput)
                {

                    wt = wt+ item.Weight;
                }
                if (wt != 100)
                {
                    return BadRequest("sum of weight is not 100");
                }
                int modelNum = _models.GetModelCount();
                modelNum = modelNum + 1;
                UserModel userModel = new UserModel()
                {
                    userId = user.Id,
                    modelId = modelNum,
                    risk = modelInputForm.risk
                };
                Console.WriteLine(modelNum);
                Console.WriteLine(user.Id);
             
              


                int modelId = _models.AddModel(userModel);
            foreach (var item in modelInputForm.modelInput)
            { 
                
                Console.WriteLine( " data1  "  +  item.Id);
                Console.WriteLine(item.Weight);
                    Console.WriteLine(modelId);
                    UserModelData userModelData = new UserModelData()
                    {
                        modelId = modelId,
                        AssetSecurityId = item.Id,
                        Weight = item.Weight
                    };

                     _models.AddUserModelData(userModelData);
             }
                return Ok("Done");
            }
            return BadRequest("Not Added");
        }

        [Authorize(Roles = "Advisor")]
        [HttpPost]
        public IActionResult AddModelToClient([FromBody] ModelToClient modelToClient ) {

            User user = GetCurrentUser();

            if (user != null)
            {
                _models.AddModelToClient(modelToClient.clientId, modelToClient.modelId);
                return Ok("Added");
            }
            return BadRequest("Not Added");
        }
    }
}
