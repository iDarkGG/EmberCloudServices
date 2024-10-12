using System.Linq.Expressions;
using EmberAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly MainContext _context;
    private readonly DbSet<T> _tabla;

    public Repository(MainContext context)
    {
        _context = context;
        _tabla = _context.Set<T>();
    }

    public async Task Add(T obj)
    {
        await _context.AddAsync(obj);
    }

    public async Task<IEnumerable<T>> GetAllAsync(bool tracking = false)
    {
        var query = _tabla.AsQueryable();
        if (tracking) query.AsNoTracking();

        return await query.ToListAsync();
    }

    public Task<T> GetAsync(Expression<Func<T, bool>> expression, bool tracking = false)
    {
        var query = _tabla.AsQueryable();
        if (tracking) query.AsNoTracking();
        
        return query.Where(expression).FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(T obj)
    {
      _tabla.Remove(obj);  
      await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}