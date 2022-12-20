using System.Linq.Expressions;
using PetAdoption.Models;

namespace PetAdoption.Data{

public class VetRepository : IRepository<Vets> 
    {    private readonly ApplicationContext applicationContext;
        public VetRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
       public Vets Get(int id)  {
            try
            {

                return applicationContext.Set<Vets>().Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Vets> GetAll(){
            try { return applicationContext.Set<Vets>().ToList(); }
            catch (Exception ex) { throw ex;}

        }
        public IEnumerable<Vets> Find(Expression<Func<Vets, bool>> predicate){
            try {return applicationContext.Set<Vets>().Where(predicate); }
            catch (Exception ex) { throw ex;}
        }
    }
 }   
