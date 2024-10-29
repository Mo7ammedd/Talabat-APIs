using Microsoft.EntityFrameworkCore;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specification;
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
        // if (typeof(T) == typeof(Product))
        // {
        //     return await _dbContext.Set<T>()
        //         .Include(p => ((Product)(object)p).Category)
        //         .Include(p => ((Product)(object)p).Brand)
        //         .FirstOrDefaultAsync(p => ((Product)(object)p).Id == id);
        // }

        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        // if (typeof(T) == typeof(Product))
        // {
        //     return await _dbContext.Set<T>()
        //         .Include(p => ((Product)(object)p).Category)
        //         .Include(p => ((Product)(object)p).Brand)
        //         .ToListAsync();
        // }

        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<int> GetCountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }


    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
    }       
}