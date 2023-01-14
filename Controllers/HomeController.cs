using Microsoft.AspNetCore.Mvc;
using PetAdoption.Models;

using System.Diagnostics;
using PetAdoption_dotnet.Models;
using PetAdoption_dotnet.Data;
using Microsoft.AspNetCore.Authorization;

namespace PetAdoption.Controllers
{  [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {   
            _logger = logger;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {  
            return View();
        }

        public IActionResult Privacy()
        {   //User.
           var u= User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}