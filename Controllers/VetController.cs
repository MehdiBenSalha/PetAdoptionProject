using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Data;
using PetAdoption.Models;
using PetAdoption.Models.DbModels;
using PetAdoption_dotnet.Data;
using PetAdoption_dotnet.Models;

namespace PetAdoption_dotnet.Controllers;
 [Authorize]
public class VetController : Controller
{
    private readonly ILogger<VetController> _logger;
   
    public VetController(ILogger<VetController> logger)
    {
        _logger = logger;
        
    }
    [AllowAnonymous]
    public IActionResult Index()
    {   
        return View();
    }    

    [Route("/VetAll")]
    public IActionResult VetAll()
    {  
        UnitOfWork u=new UnitOfWork(ApplicationContext.Instance);
        IEnumerable<Vet> vets = u.Vet.GetAll();
        
        //Vets v= repository.Get(1);
       
    
        return View(vets);
    }
      [Route("/Vet/Add")]
      public IActionResult Add()
    {  
        return View();
    }
       [Route("/Vet/Addform")]
        [HttpPost]
        public IActionResult Addform(Vet vet)
        {    UnitOfWork u=new UnitOfWork(ApplicationContext.Instance);
             vet.userrId=User.Claims.FirstOrDefault(c => c.Type =="http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
             ((VetRepository)u.Vet).Insert(vet);
            u.Complete();
             return  RedirectToAction("VetAll");
        }
        /*public IActionResult NewAccount()
        {
               return  RedirectToAction("Index","Home"); 
        }*/
        
        [Route("/Vet/delete/{id?}")]
        public IActionResult delete(int id)
        {   UnitOfWork u=new UnitOfWork(ApplicationContext.Instance); 
             Vet v=((VetRepository)u.Vet).Get(id);
             if(v!=null && User.Identity.IsAuthenticated ) 
        { var str=User.Claims.FirstOrDefault(c => c.Type =="http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
            if(v.userrId==str)
            {  ((VetRepository)u.Vet).Delete(id);
                u.Complete();
                 TempData["warning"]="  success delete ";
                 return  RedirectToAction("VetAll");
            }
             }
             TempData["warning"]="  access denied";
             return  RedirectToAction("VetAll");
        }
        [Route("/Vet/update/{id?}")]
        public IActionResult update(int id)
        {   UnitOfWork u=new UnitOfWork(ApplicationContext.Instance); 
             Vet v=((VetRepository)u.Vet).Get(id);
             if(v!=null && User.Identity.IsAuthenticated ) 
        { var str=User.Claims.FirstOrDefault(c => c.Type =="http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
            if(v.userrId==str)
            {  
              return  View(v);
            }
             }
             TempData["warning"]="  access denied";
             return  RedirectToAction("VetAll");
        }
        
        [Route("/Vet/Updateformmm")]
        [HttpPost]
        

        public IActionResult Updateformmm(Vet v)
        {   UnitOfWork u=new UnitOfWork(ApplicationContext.Instance);
              Vet vi=((VetRepository)u.Vet).Get(v.vetId);
              vi.name=v.name; 
              vi.adress=v.adress;
              vi.img=v.img;
              vi.localisation=v.localisation;
             var repository=new VetRepository(ApplicationContext.Instance);
             ((VetRepository)u.Vet).Update(vi);
             u.Complete();
            
             TempData["warning"]="  access update";
             return  RedirectToAction("VetAll");
        }

        

    [Route("/Vet/{id?}")]
    public IActionResult VetById(int id)
    {   
       UnitOfWork u=new UnitOfWork(ApplicationContext.Instance);
        Vet vet=((VetRepository)u.Vet).Get(id);
        if(vet==null){
            //TempData["warning"]="   Invalid id ";
            return RedirectToAction("VetAll");
        }
        return View(vet);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
