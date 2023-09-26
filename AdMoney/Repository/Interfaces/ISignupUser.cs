using AdMoney.Models;

namespace AdMoney.Repository.Interfaces
{
    public interface ISignupUser
    {
        public int AddSignupUser(User user);
        public bool checkAdminUser(string email);



    }
}
