using AdMoney.Models;

namespace AdMoney.Repository.Interfaces
{
    public interface IModels
    {
        public List<AssetSecurity> GetAllAssetSecurity();
        public int GetModelCount();
        public int AddModel(UserModel userModel);

        public void AddUserModelData(UserModelData userModelData);

        public void AddModelToClient(int clientId, int modelId);
    }
}
