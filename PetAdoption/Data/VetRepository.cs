using System.Linq.Expressions;
using PetAdoption_dotnet.Data;
using PetAdoption_dotnet.Models;

namespace PetAdoption_dotnet.Data{

public class VetRepository : IRepository<Vets> 
    {    private readonly ApplicationContext applicationContext;
        public VetRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
            
        }
         public void Save()
        {
            this.applicationContext.SaveChanges();
        }
        public void Insert(Vets veterinaire)
        {  
           this.applicationContext.vets.Add(veterinaire);
        }
        public void Delete(int vetID)
        {
            Vets vet = applicationContext.vets.Find(vetID);
            applicationContext.vets.Remove(vet);
        }
       public Vets Get(int id)  {
            try
            {

                return applicationContext.vets.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Vets> GetAll(){
            try { return applicationContext.vets.ToList(); }
            catch (Exception ex) { throw ex;}
        
        }
       
        public IEnumerable<Vets> Find(Expression<Func<Vets, bool>> predicate){
            try {return applicationContext.vets.Where(predicate); }
            catch (Exception ex) { throw ex;}
        }
    }
 }   
