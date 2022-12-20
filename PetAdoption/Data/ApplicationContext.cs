using Microsoft.EntityFrameworkCore;
using PetAdoption.Models;
using System.Diagnostics;

namespace PetAdoption.Data
{

public class ApplicationContext : DbContext {
    private static ApplicationContext? _contextinstance=null;
    public static ApplicationContext Instance{
        get
        {
             if(_contextinstance==null)
             {var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
              // to get the absolute path of db
              string fullPath = Path.GetFullPath(".");
              // to get the absolute path of db
              optionBuilder.UseSqlite("Data Source="+fullPath+"/db/db1.db");
              _contextinstance=new ApplicationContext(optionBuilder.Options);
             }
             return _contextinstance;
        }
        
        
    }
    private ApplicationContext(DbContextOptions o) : base(o){ 
        Debug.WriteLine("context    instantiation ");
    }
   public DbSet<Vets> Vets {get; set;} 

}
}
