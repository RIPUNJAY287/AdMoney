using AdMoney.Data;
using AdMoney.Models;
using AdMoney.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AdMoney.Repository.Implementation
{
    public class SignupUser : ISignupUser
    {
        private readonly AdMoneyContext _context;
        public SignupUser(AdMoneyContext context) { 
            _context = context;
        }

        public int AddSignupUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public bool checkAdminUser(string email)
        {
            Console.WriteLine("emaiil admin " + email );
            var usr = _context.AdminUsers.Where(user => user.Email ==email).FirstOrDefault();
            if(usr != null) 
            {
                return true;
            }
            return false;
        }

    
    }
}
