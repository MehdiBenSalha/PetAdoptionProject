using Microsoft.EntityFrameworkCore;
using PetAdoption_dotnet.Models;
using System.Diagnostics;

namespace PetAdoption_dotnet.Data
{

public class ApplicationContext : DbContext {
    private static ApplicationContext? _contextinstance=null;
    public static ApplicationContext Instance{
        get
        {
             if(_contextinstance==null)
             {var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
              optionBuilder.UseSqlite("Data Source=C:/Users/user/Documents/vscode_proj/charpproj/PetAdoption_dotnet/db/db.db");
              _contextinstance=new ApplicationContext(optionBuilder.Options);
             }
             return _contextinstance;
        }
        
        
    }
    private ApplicationContext(DbContextOptions o) : base(o){ 
        Debug.WriteLine("context    instantiation ");
    }
   public DbSet<Vet>? Vet {get; set;} 

}
}
