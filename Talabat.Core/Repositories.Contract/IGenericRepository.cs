using Talabat.Core.Models;

namespace Talabat.Core.Repositories.Contract;

public interface IGenericRepository<T> where T : BaseModel
{
    Task<T?> GetAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}