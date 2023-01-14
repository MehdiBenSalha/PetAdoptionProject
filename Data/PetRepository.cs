using Microsoft.EntityFrameworkCore;
using PetAdoption.Models;
using PetAdoption.Models.DbModels;
using PetAdoption_dotnet.Data;
using System.Linq.Expressions;

namespace PetAdoption.Data
{
    public class PetRepository : IRepository<Pet>
    {
        private readonly ApplicationContext applicationContext;
        public PetRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;

        }
        public void Save()
        {
            this.applicationContext.SaveChanges();
        }
        public void Insert(Pet animal)
        {
            this.applicationContext.Pet.Add(animal);
        }
        public void Delete(int petId)
        {
            Pet animal = applicationContext.Pet.Find(petId);
            applicationContext.Pet.Remove(animal);
        }
        public void Update(Pet animal)
        {
            applicationContext.Entry(animal).State = EntityState.Modified;
        }
        public Pet Get(int petId)
        {
            try
            {

                return applicationContext.Pet.Find(petId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Pet> Find(Expression<Func<Pet, bool>> predicate)
        {
            try { return applicationContext.Pet.Where(predicate); }
            catch (Exception ex) { throw ex; }
        }

        public IEnumerable<Pet> GetAll()
        {
            try { return applicationContext.Pet.ToList(); }
            catch (Exception ex) { throw ex; }
        }
    }
}

