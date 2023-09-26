using AdMoney.Models;
using AdMoney.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;

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
            _loginUser = loginUser ?? throw new ArgumentNullException(nameof(loginUser)); 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            /*  ClaimsPrincipal claimUser = HttpContext.User;
              if(claimUser.Identity.IsAuthenticated)
              {
                  return RedirectToAction("Index", "Home");
              }*/
            ViewData["check"] = "false";
            ViewData["access"] = "Not admin access";
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Login(UserLogin userLogin)
        {
            Console.WriteLine("helloo " + userLogin.email + " " + userLogin.password + "  ->" + userLogin.role);
            if (userLogin == null )
            {
                ViewData["check"] = "true";
                ViewData["access"] = "Not admin access";
                return View();
            }

            Console.WriteLine("helloo " + userLogin.email + " " + userLogin.password + "  ->" + userLogin.role + "<-");


            User user  = _loginUser.CheckValidUser(userLogin.email, userLogin.password,userLogin.role);
            
            if (user != null)
            {

                List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(ClaimTypes.Role,user.Role)
                    };

                ClaimsIdentity claimsIdentity  =  new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                if(user.Role == "Admin")
                {
                    return RedirectToAction("AdminInfo", "Admin");
                }
                return RedirectToAction("Index", "Advisor");
            }
            ViewData["check"] = "true";
            ViewData["access"] = "Not Valid access";
            return View();
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(UserSignup userSignup)
        {
            if(userSignup!= null && userSignup.Role == "Admin")
            {
               if( !_signupUser.checkAdminUser(userSignup.Email))
                {
                    ViewData["check"] = "false";
                    return View();
                }

            }
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