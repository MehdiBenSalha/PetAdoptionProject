using PetAdoption.Models.DbModels;
using PetAdoption_dotnet.Data;

namespace PetAdoption.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Userr> User { get; }
        IRepository<Pet> Pet { get; }   
        IRepository<Vet> Vet { get; }
        IRepository<CentreAdressage> CentreAdressage { get;}
    }
}
