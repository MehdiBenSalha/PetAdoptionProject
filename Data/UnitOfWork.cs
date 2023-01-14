using PetAdoption.Models.DbModels;
using PetAdoption_dotnet.Data;

namespace PetAdoption.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContextDbContext;
        public IRepository<Userr> User { get; private set; }
        public IRepository<Pet> Pet { get; private set; }
        public IRepository<Vet> Vet { get; private set; }
        public IRepository<CentreAdressage> CentreAdressage { get; private set; }
        public UnitOfWork(ApplicationContext applicationContext)
        {
            this._applicationContextDbContext = applicationContext;
            User = new UserrRepository(applicationContext);
            Pet= new PetRepository(applicationContext); 
            Vet= new VetRepository(applicationContext);
            CentreAdressage = new CentreAdressageRepository(applicationContext);
            
        }
        public bool Complete()
        {
            try
            {
                int result=_applicationContextDbContext.SaveChanges();
                if(result > 0 ) { 
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Dispose()
        {
            _applicationContextDbContext.Dispose();
        }
    }
}
