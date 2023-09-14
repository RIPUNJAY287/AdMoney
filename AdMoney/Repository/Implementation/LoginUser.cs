using AdMoney.Data;
using AdMoney.Models;
using AdMoney.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AdMoney.Repository.Implementation
{
    public class LoginUser : ILoginUser
    {
        private readonly AdMoneyContext _context;
        public LoginUser(AdMoneyContext context) {
            _context = context;
        }
        public int CheckValidUser(string email, string password)
        {
            User? user = _context.Users.Where(user=> user.Email == email && user.Password == password).FirstOrDefault();
            
            if (user != null)
            {
                return user.Id;
            }
            return 0 ;
        }
    }
}
