using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Models;
using PetAdoption_dotnet.Data;
using PetAdoption_dotnet.Models;

namespace PetAdoption_dotnet.Controllers;

public class VetController : Controller
{
    private readonly ILogger<VetController> _logger;

    public VetController(ILogger<VetController> logger)
    {
        _logger = logger;
        
    }

    public IActionResult Index()
    {   
        return View();
    }    

    [Route("/Vet")]
    public IActionResult Vet()
    {  
        var repository=new VetRepository(ApplicationContext.Instance);
        IEnumerable <Vet> vets =repository.GetAll();
        
        //Vets v= repository.Get(1);
       
    
        return View(vets);
    }
     [Route("/Vet/add")]
        public IActionResult add()
    {  
        var repository=new VetRepository(ApplicationContext.Instance);
        Vet v=new Vet();
        v.name=44;v.adress="alert";v.mail="id";
        repository.Insert(v);
        repository.Save();
        //v.adress="talaer";
        //repository.Insert(v);
       
    
        return RedirectToAction("Vet");
    }

    [Route("/Vet/{id?}")]
    public IActionResult VetById(int id)
    {   
        var repository=new VetRepository(ApplicationContext.Instance);
        Vet vet =repository.Get(id);
        if(vet==null){
            TempData["warning"]="   Invalid id ";
            return RedirectToAction("Vet");
        }
        return View(vet);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
