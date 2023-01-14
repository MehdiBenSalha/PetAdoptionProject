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
public class PetController : Controller
{
    private readonly ILogger<PetController> _logger;

    public PetController(ILogger<PetController> logger)
    {
        _logger = logger;

    }
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/PetAll")]
    public IActionResult PetAll()
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        IEnumerable<Pet> pets = u.Pet.GetAll();

        //Vets v= repository.Get(1);


        return View(pets);
    }
    [Route("/Pet/Add")]
    public IActionResult Add()
    {
        return View();
    }
    [Route("/Pet/Addform")]
    [HttpPost]
    public IActionResult Addform(Pet pet)
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        pet.userrId = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
        ((PetRepository)u.Pet).Insert(pet);
        u.Complete();
        return RedirectToAction("PetAll");
    }
    /*public IActionResult NewAccount()
    {
           return  RedirectToAction("Index","Home"); 
    }*/

    [Route("/Pet/delete/{id?}")]
    public IActionResult delete(int id)
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        Pet v = ((PetRepository)u.Pet).Get(id);
        if (v != null && User.Identity.IsAuthenticated)
        {
            var str = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
            if (v.userrId == str)
            {
                ((PetRepository)u.Pet).Delete(id);
                u.Complete();
                TempData["warning"] = "  success delete ";
                return RedirectToAction("PetAll");
            }
        }
        TempData["warning"] = "  access denied";
        return RedirectToAction("PetAll");
    }
    [Route("/Pet/update/{id?}")]
    public IActionResult update(int id)
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        Pet v = ((PetRepository)u.Pet).Get(id);
        if (v != null && User.Identity.IsAuthenticated)
        {
            var str = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
            if (v.userrId == str)
            {
                return View(v);
            }
        }
        TempData["warning"] = "  access denied";
        return RedirectToAction("PetAll");
    }


    [HttpPost]


    public IActionResult Updateformmm(Pet v)
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        //Vet vi=((VetRepository)u.Vet).Get(v.vetId); 

        ((PetRepository)u.Pet).Update(v);
        u.Complete();

        TempData["warning"] = "  access update";
        return RedirectToAction("PetAll");
    }



    [Route("/Pet/{id?}")]
    public IActionResult PetById(int id)
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        Pet pet = ((PetRepository)u.Pet).Get(id);
        if (pet == null)
        {
            //TempData["warning"]="   Invalid id ";
            return RedirectToAction("PetAll");
        }
        return View(pet);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
