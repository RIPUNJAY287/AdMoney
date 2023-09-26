using AdMoney.Data;
using AdMoney.Models;
using AdMoney.Repository.Interfaces;
using System.ComponentModel;

namespace AdMoney.Repository.Implementation
{
    public class AdvisorClientData : IAdvisorClientData
    {
        private readonly AdMoneyContext _context;
        public AdvisorClientData(AdMoneyContext context)
        {
            _context = context;
        }

        public void AddNewClient(Client client)
        {

            _context.Clients.Add(client);
            _context.SaveChanges();

        }


        public User GetAdvisorProfile(int Id)
        {
            return _context.Users.Where(u => u.Id == Id).FirstOrDefault();
        }

        public List<Client> getAllAddedClients(int userId)
        {
            List<Client> clients = _context.Clients.Where(c => c.modelId != null && c.AdvisorId == userId).ToList();
            return clients;
        }

        public List<Client> getAllClients(int userId)
        {
            List<Client> clients = _context.Clients.Where(c => c.modelId == null && c.AdvisorId == userId).ToList();
            return clients;
        }

        public List<ModelSelectData> GetAllModels(string ris, int userId)
        {
            Console.WriteLine("rsi k " + ris + "   " + userId);

            Console.WriteLine(ris + "   " + userId);
            var userModelList = from usermdl in _context.UserModels
                                join usermdldata in _context.UserModelsData on usermdl.modelId equals usermdldata.modelId
                                join assetSec in _context.AssetSecurity on usermdldata.AssetSecurityId equals assetSec.Id
                                where usermdl.userId == userId && usermdl.risk == ris
                                select new
                                {
                                    modId = usermdldata.modelId,
                                    riskProfile = usermdl.risk,
                                    assetId = assetSec.Id,
                                    assetName = assetSec.Asset,
                                    securityName = assetSec.SecurityName,
                                    weight = usermdldata.Weight
                                };


            List<ModelSelectData> modelList = new List<ModelSelectData>();
            int check = 0;
            ModelSelectData? modelSelectData = null;
            List<AssetInfo> assetInfos = null;

            foreach (var obj in userModelList)
            {
                if (check != obj.modId)
                {
                    if (check != 0)
                    {
                        Console.WriteLine(" check " + check);
                        if (modelSelectData != null)
                        {
                            modelSelectData.assetInfo = assetInfos;
                            modelList.Add(modelSelectData);
                        }
                    }
                    check = obj.modId;
                    modelSelectData = new ModelSelectData();
                    assetInfos = new List<AssetInfo>();
                    modelSelectData.modelId = obj.modId;
                    modelSelectData.riskProfile = obj.riskProfile;
                }
                AssetInfo assetInfo = new AssetInfo();
                assetInfo.assetId = obj.assetId;
                assetInfo.assetName = obj.assetName;
                assetInfo.securityName = obj.securityName;
                assetInfo.weight = obj.weight;
                if (assetInfos != null)
                {
                    assetInfos.Add(assetInfo);

                }
            }
            if (modelSelectData != null)
            {
                modelSelectData.assetInfo = assetInfos;
                modelList.Add(modelSelectData);
            }
            return modelList;
        }

        public Client GetClientById(int Id)
        {
            return _context.Clients.Where(c => c.Id == Id).FirstOrDefault();

        }
    }
}
