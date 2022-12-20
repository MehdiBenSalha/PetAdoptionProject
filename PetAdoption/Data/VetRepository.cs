using System.Linq.Expressions;
using PetAdoption_dotnet.Data;
using PetAdoption_dotnet.Models;

namespace PetAdoption_dotnet.Data{

public class VetRepository : IRepository<Vet> 
    {    private readonly ApplicationContext applicationContext;
        public VetRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
       public Vet Get(int id)  {
            try
            {

                return applicationContext.Set<Vet>().Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Vet> GetAll(){
            try { return applicationContext.Set<Vet>().ToList(); }
            catch (Exception ex) { throw ex;}

        }
        public IEnumerable<Vet> Find(Expression<Func<Vet, bool>> predicate){
            try {return applicationContext.Set<Vet>().Where(predicate); }
            catch (Exception ex) { throw ex;}
        }
    }
 }   
