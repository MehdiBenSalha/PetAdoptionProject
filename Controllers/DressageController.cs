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
public class DressageController : Controller
{
    private readonly ILogger<DressageController> _logger;

    public DressageController(ILogger<DressageController> logger)
    {
        _logger = logger;

    }
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/DressageAll")]
    public IActionResult DressageAll()
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        IEnumerable<CentreAdressage> CentreAdressages = u.CentreAdressage.GetAll();

        //Vets v= repository.Get(1);


        return View(CentreAdressages);
    }
    [Route("/CentreAdressage/Add")]
    public IActionResult Add()
    {
        return View();
    }
    [Route("/CentreAdressage/Addform")]
    [HttpPost]
    public IActionResult Addform(CentreAdressage centreAdressage)
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        centreAdressage.userrId = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
        ((CentreAdressageRepository)u.CentreAdressage).Insert(centreAdressage);
        u.Complete();
        return RedirectToAction("DressageAll");
    }
    /*public IActionResult NewAccount()
    {
           return  RedirectToAction("Index","Home"); 
    }*/

    [Route("/CentreAdressage/delete/{id?}")]
    public IActionResult delete(int id)
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        CentreAdressage v = ((CentreAdressageRepository)u.CentreAdressage).Get(id);
        if (v != null && User.Identity.IsAuthenticated)
        {
            var str = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
            if (v.userrId == str)
            {
                ((CentreAdressageRepository)u.CentreAdressage).Delete(id);
                u.Complete();
                TempData["warning"] = "  success delete ";
                return RedirectToAction("CentreAdressageAll");
            }
        }
        TempData["warning"] = "  access denied";
        return RedirectToAction("CentreAdressageAll");
    }
    [Route("/CentreAdressage/update/{id?}")]
    public IActionResult update(int id)
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        CentreAdressage v = ((CentreAdressageRepository)u.CentreAdressage).Get(id);
        if (v != null && User.Identity.IsAuthenticated)
        {
            var str = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
            if (v.userrId == str)
            {
                return View(v);
            }
        }
        TempData["warning"] = "  access denied";
        return RedirectToAction("CentreAdressageAll");
    }


    [HttpPost]


    public IActionResult Updateformmm(CentreAdressage v)
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        //Vet vi=((VetRepository)u.Vet).Get(v.vetId); 

        ((CentreAdressageRepository)u.CentreAdressage).Update(v);
        u.Complete();

        TempData["warning"] = "  access update";
        return RedirectToAction("CentreAdressageAll");
    }



    [Route("/CentreAdressage/{id?}")]
    public IActionResult CentreAdressageById(int id)
    {
        UnitOfWork u = new UnitOfWork(ApplicationContext.Instance);
        CentreAdressage centreAdressage = ((CentreAdressageRepository)u.CentreAdressage).Get(id);
        if (centreAdressage == null)
        {
            //TempData["warning"]="   Invalid id ";
            return RedirectToAction("CentreAdressageAll");
        }
        return View(centreAdressage);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
