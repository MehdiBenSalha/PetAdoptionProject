using Microsoft.EntityFrameworkCore;
using PetAdoption.Models;
using PetAdoption.Models.DbModels;
using PetAdoption_dotnet.Data;
using PetAdoption_dotnet.Models;
using System.Linq.Expressions;

namespace PetAdoption.Data
{
    public class CentreAdressageRepository : IRepository<CentreAdressage>
    {
        private readonly ApplicationContext applicationContext;
        public CentreAdressageRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;

        }
        public void Save()
        {
            this.applicationContext.SaveChanges();
        }
        public void Insert(CentreAdressage centre)
        {
            this.applicationContext.CentreAdressage.Add(centre);
        }
        public void Delete(int centreAdressageId)
        {
            CentreAdressage centre = applicationContext.CentreAdressage.Find(centreAdressageId);
            applicationContext.CentreAdressage.Remove(centre);
        }
         public void Update(CentreAdressage center)
        {
            applicationContext.Entry(center).State = EntityState.Modified;
        }
        public CentreAdressage Get(int centreAdressageId)
        {
            try
            {

                return applicationContext.CentreAdressage.Find(centreAdressageId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<CentreAdressage> GetAll()
        {
            try { return applicationContext.CentreAdressage.ToList(); }
            catch (Exception ex) { throw ex; }

        }

        public IEnumerable<CentreAdressage> Find(Expression<Func<CentreAdressage, bool>> predicate)
        {
            try { return applicationContext.CentreAdressage.Where(predicate); }
            catch (Exception ex) { throw ex; }
        }
    }
}
