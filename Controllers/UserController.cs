using Microsoft.AspNetCore.Mvc;
using PetAdoption.Models;

using System.Diagnostics;
using PetAdoption_dotnet.Models;
using PetAdoption.Data;
using Microsoft.AspNetCore.Authorization;
using PetAdoption.Models.DbModels;
using PetAdoption_dotnet.Data;

namespace PetAdoption.Controllers
{  [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        
        public UserController(ILogger<UserController> logger)
        {   
            _logger = logger;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {   
            return View();
        }
        [Route("/User")]
    public IActionResult UserAll()
    {  
        var repository=new UserrRepository(ApplicationContext.Instance);
        IEnumerable <Userr> users =repository.GetAll();
        
        //Vets v= repository.Get(1);
       
    
        return View(users);
    }
    [Route("/User/update/{id?}")]
    public IActionResult UserUpdate(string id)
    {  
        var repository=new UserrRepository(ApplicationContext.Instance);
        var u= repository.Get(id);
        u.adress="azizz";
        u.name="aziz";
        u.number="aziz";
        repository.Update(u);
        repository.Save();
        
        //Vets v= repository.Get(1);
       
    
        return RedirectToAction("UserById","User",u.userrId);
    }
 [Route("/User/{id?}")]
    public IActionResult UserById(string id)
    {   UnitOfWork u=new UnitOfWork(ApplicationContext.Instance); 
             Userr user=((UserrRepository)u.User).Get(id);
             if(user!=null && User.Identity.IsAuthenticated ) 
        { var str=User.Claims.FirstOrDefault(c => c.Type =="http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
            if(user.userrId==str)
            {   user.pets=((UserrRepository)u.User).getMyPets(user.userrId); 
                user.vett=((UserrRepository)u.User).getMyVet(user.userrId);
                 return View(user);
            }
             }
           
        
             TempData["warning"]="  access denied";
             return  RedirectToAction("UserAll");
        
        
        
       
        
        
    }
     
        public IActionResult Privacy()
        {   //User.
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}