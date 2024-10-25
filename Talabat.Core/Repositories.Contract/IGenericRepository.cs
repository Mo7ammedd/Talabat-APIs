using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Core.Repositories.Contract;

public interface IGenericRepository<T> where T : BaseModel
{
    Task<T?> GetAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    
    Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
    
    Task<T?> GetWithSpecAsync(ISpecification<T> spec);
    
    Task<int> GetCountAsync(ISpecification<T> spec);

}