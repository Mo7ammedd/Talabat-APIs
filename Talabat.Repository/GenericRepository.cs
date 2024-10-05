using Microsoft.EntityFrameworkCore;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    private readonly StoreContext _dbContext;

    public GenericRepository(StoreContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetAsync(int id)
    {
        if (typeof(T) == typeof(Product))
        {
            return await _dbContext.Set<T>()
                .Include(p => ((Product)(object)p).Category)
                .Include(p => ((Product)(object)p).Brand)
                .FirstOrDefaultAsync(p => ((Product)(object)p).Id == id);
        }

        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        if (typeof(T) == typeof(Product))
        {
            return await _dbContext.Set<T>()
                .Include(p => ((Product)(object)p).Category)
                .Include(p => ((Product)(object)p).Brand)
                .ToListAsync();
        }

        return await _dbContext.Set<T>().ToListAsync();
    }
}