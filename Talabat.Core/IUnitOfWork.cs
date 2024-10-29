using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Core;

public interface IUnitOfWork : IAsyncDisposable
{

    IGenericRepository<T> Repository<T>() where T : BaseModel; 
    
    Task<int> CompleteAsync();
    
}