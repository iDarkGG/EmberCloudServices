using System.Linq.Expressions;
namespace EmberAPI.Repositories;

public interface IRepository<T> where T : class 
{
    Task<IEnumerable<T>> GetAllAsync(bool tracking = false);

    Task<T> GetAsync(Expression<Func<T, bool>> expression, bool tracking = false);
    
    Task Add(T obj);
    Task DeleteAsync(T obj);
    
    Task SaveChangesAsync();
}