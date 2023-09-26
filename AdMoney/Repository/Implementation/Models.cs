using AdMoney.Data;
using AdMoney.Models;
using AdMoney.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace AdMoney.Repository.Implementation
{
    public class Models : IModels
    {
        private readonly AdMoneyContext _context;
        public Models(AdMoneyContext context) { _context = context; }





        public int AddModel(UserModel userModel)
        {

            _context.UserModels.Add(userModel);
            _context.SaveChanges();
            return userModel.modelId;
        }

        public void AddModelToClient(int clientId,int modelId)
        {
            var cli = _context.Clients.Where(c => c.Id == clientId).FirstOrDefault();
            if (cli != null)
            {
                cli.modelId = modelId;
            }
            _context.SaveChanges();
        }

        public void AddUserModelData(UserModelData userModelData)
        {
            _context.UserModelsData.Add(userModelData);
            _context.SaveChanges();
        }

        public List<AssetSecurity> GetAllAssetSecurity()
        {
            List<AssetSecurity> assetsSecurity = _context.AssetSecurity.ToList();
            return assetsSecurity;
        }

        public int GetModelCount()
        {
            List<int> li =  _context.UserModels.Select(p => p.Id).ToList();
            int last = 0;
            foreach(int l in li)
            {
                last = l;
            }
            return last;
        }
    }
}
