using AdMoney.Models;
using AdMoney.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace AdMoney.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISignupUser _signupUser;
        private readonly ILoginUser _loginUser;

        public HomeController(ILogger<HomeController> logger, ISignupUser signupUser, ILoginUser loginUser)
        {
            _signupUser = signupUser ?? throw new ArgumentNullException(nameof(signupUser));
            _logger = logger;
            _loginUser = loginUser ?? throw new ArgumentNullException(nameof(loginUser)); ;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        { 
            if(userLogin == null)
            {
                return View();
            }

            Console.WriteLine("helloo " + userLogin.email + " " + userLogin.password);
            int Id  = _loginUser.CheckValidUser(userLogin.email, userLogin.password);
            Console.WriteLine(Id + " id ");
            if (Id != 0)
            {
               return RedirectToAction("Index", "Home");
            }
            return View();
        }
        
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(UserSignup userSignup)
        {
            Console.WriteLine(userSignup.Name  + " " + userSignup.Email + " " + userSignup.Password + " " + userSignup.Role) ;
            User user = new User()
            {
                Name = userSignup.Name,
                Email = userSignup.Email,
                Password = userSignup.Password,
                Role = userSignup.Role
            };
            _signupUser.AddSignupUser(user);
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}