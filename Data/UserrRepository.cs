using Microsoft.EntityFrameworkCore;
using PetAdoption.Models;
using PetAdoption.Models.DbModels;
using PetAdoption_dotnet.Data;
using PetAdoption_dotnet.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PetAdoption.Data
{
    public class UserrRepository : IRepository<Userr>
    {
        private readonly ApplicationContext applicationContext;
        public UserrRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;

        }
        public IEnumerable<Pet> getMyPets(string id){
            try { return applicationContext.Pet.Where(vet=>vet.userrId==id).ToList(); }
            catch (Exception ex) { throw ex; }
        }
        public IEnumerable<Vet> getMyVet(string id){
            try { return applicationContext.Vet.Where(vet=>vet.userrId==id).ToList(); }
            catch (Exception ex) { throw ex; }
        }
        public IEnumerable<CentreAdressage> getMyCentreD(string id){
            try { return applicationContext.CentreAdressage.Where(vet=>vet.userrId==id).ToList(); }
            catch (Exception ex) { throw ex; }
        }
        public void Save()
        {
            this.applicationContext.SaveChanges();
        }
        public void Insert(Userr utilisateur)
        {
            this.applicationContext.Userr.Add(utilisateur);
        }
        public void Delete(string userId)
        {
            Userr utilisateur = applicationContext.Userr.Find(userId);
            applicationContext.Userr.Remove(utilisateur);
        }
         public void Update(Userr user)
        {
            applicationContext.Entry(user).State = EntityState.Modified;
        }
        public Userr Get(string userId)
        {
            try
            {

                return applicationContext.Userr.Find(userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Userr> Find(Expression<Func<Userr, bool>> predicate)
        {
            try { return applicationContext.Userr.Where(predicate); }
            catch (Exception ex) { throw ex; }
        }

        public IEnumerable<Userr> GetAll()
        {
            try { return applicationContext.Userr.ToList(); }
            catch (Exception ex) { throw ex; }
        }
    }
}
