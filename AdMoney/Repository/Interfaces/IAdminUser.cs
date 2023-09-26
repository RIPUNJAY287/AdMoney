using AdMoney.Models;

namespace AdMoney.Repository.Interfaces
{
    public interface IAdminUser
    {
        public List<AssetSecurity> GetAllAssetSecurities()
        {
            return new List<AssetSecurity>();
        }
        public void AddAssetSecurity(AssetSecurity assetSecurity);
    }
}
