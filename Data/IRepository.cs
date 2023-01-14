using System.Linq.Expressions;

namespace PetAdoption_dotnet.Data{
public interface IRepository<TEntity> where TEntity : class 
    {   
    
       void Save();
       IEnumerable<TEntity> GetAll();
       IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}