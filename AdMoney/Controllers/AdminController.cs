using AdMoney.Models;
using AdMoney.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AdMoney.Controllers
{
    public class AdminController : Controller
    {

        private readonly ILogger<AdvisorController> _logger;
        private readonly IAdminUser _admin;
   
        public AdminController(ILogger<AdvisorController> logger, IAdminUser admin)
        {
            _logger = logger;
            _admin = admin;
        }
        /*    public IActionResult Index()
            {
                return View();
            }*/
        [Authorize(Roles = "Admin")]
        public IActionResult AdminInfo()
        {
            List<AssetSecurity> models = _admin.GetAllAssetSecurities().ToList();
            ViewData["AssetSecurityList"] = models;
            

            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddAssetSecurities([FromBody] AssetSecurity assetSecurity)
        {   
            Console.WriteLine(assetSecurity.Asset +  " ----------- " + assetSecurity.SecurityName);
            _admin.AddAssetSecurity(assetSecurity);
            return Ok("Added");
        }


    }
}
