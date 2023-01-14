using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Models.DbModels;
using PetAdoption_dotnet.Data;
using PetAdoption_dotnet.Models;

namespace PetAdoption.Data{

public class VetRepository : IRepository<Vet> 
    {   
        private readonly ApplicationContext applicationContext;
        public VetRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
            
        }
        public void Save()
        {
            this.applicationContext.SaveChanges();
        }
        public void Insert(Vet veterinaire)
        {  
           this.applicationContext.Vet.Add(veterinaire);
        }
        public void Delete(int vetID)
        {
            Vet vet = applicationContext.Vet.Find(vetID);
            applicationContext.Vet.Remove(vet);
        }
         public void Update(Vet animal)
        {
            applicationContext.Entry(animal).State = EntityState.Modified;
        }
       public Vet Get(int id)  {
            try
            {

                return applicationContext.Vet.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Vet> GetAll(){
            try { return applicationContext.Vet.ToList(); }
            catch (Exception ex) { throw ex;}
        
        }
       
        public IEnumerable<Vet> Find(Expression<Func<Vet, bool>> predicate){
            try {return applicationContext.Vet.Where(predicate); }
            catch (Exception ex) { throw ex;}
        }
    }
 }   
