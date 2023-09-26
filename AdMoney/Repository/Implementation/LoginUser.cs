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
        public User CheckValidUser(string email, string password, string role)
        {
            User? user = _context.Users.Where(user=> user.Email == email && user.Password == password && user.Role == role).FirstOrDefault();
            
            if (user != null)
            {
                return user;
            }
            return user;
        }
    }
}
