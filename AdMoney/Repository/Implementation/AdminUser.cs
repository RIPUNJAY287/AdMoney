using AdMoney.Data;
using AdMoney.Models;
using AdMoney.Repository.Interfaces;

namespace AdMoney.Repository.Implementation
{
    public class AdminUser : IAdminUser
    {

        private readonly AdMoneyContext _context;
        public AdminUser(AdMoneyContext context)
        {
            _context = context;
        }
        public List<AssetSecurity> GetAllAssetSecurities()
        {
            List<AssetSecurity> assetsSecurity = _context.AssetSecurity.ToList();
            return assetsSecurity;
        }

        public void AddQuestion()
        {

        }

        public void AddAssetSecurity(AssetSecurity assetSecurity)
        {
            _context.AssetSecurity.Add(assetSecurity);
            _context.SaveChanges();
        }
    }
}
