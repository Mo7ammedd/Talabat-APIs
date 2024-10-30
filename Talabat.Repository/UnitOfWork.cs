using System.Collections;
using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext _dbContext;
    private readonly Hashtable _repositories;

    public UnitOfWork(StoreContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Hashtable();
    }


    public IGenericRepository<T> Repository<T>() where T : BaseModel
    {
        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(T));
            var repositoryInstance = Activator.CreateInstance(repositoryType, _dbContext);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public Task<int> CompleteAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
