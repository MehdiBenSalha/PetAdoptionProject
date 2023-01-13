using Microsoft.EntityFrameworkCore;
using PetAdoption_dotnet.Models;
using System.Diagnostics;
//using MySql.Data.MySqlClient;
using MySql.Data.EntityFrameworkCore;



namespace PetAdoption_dotnet.Data
{

public class ApplicationContext : DbContext {
    private static ApplicationContext? _contextinstance=null;
    public static ApplicationContext Instance{
        get
        {
             if(_contextinstance==null)
             {var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
              string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=test;";
              // DATABASE_URL="mysql://root:@127.0.0.1:3306/project_symf1?serverVersion=10.4.22-MariaDB&charset=utf8mb4"
            optionBuilder.UseMySQL(connectionString);
             //optionBuilder.UseSqlite("Data Source=C:/Users/user/Documents/vscode_proj/charpproj/PetAdoption_dotnet/db/db1.db");
              _contextinstance=new ApplicationContext(optionBuilder.Options);
              _contextinstance.Database.EnsureCreated();
             }
             return _contextinstance;
        }
        
        
    }
    private ApplicationContext(DbContextOptions o) : base(o){ 
        Debug.WriteLine("context    instantiation ");
    }
   public DbSet<Vets> vets {get; set;} 
   

}
}
