using AdMoney.Models;

namespace AdMoney.Repository.Interfaces
{
    public interface ILoginUser
    {
        public  User CheckValidUser(string username, string password,string role);

    }
}
