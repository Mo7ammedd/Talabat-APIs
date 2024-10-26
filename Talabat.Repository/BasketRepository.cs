using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository;

public class BasketRepository : IBasketRepository
{
    public BasketRepository()
    {
    }
    public Task<CustomerBasket> GetBasketAsync(string basketId)
    {
    }

    public Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
    }

    public Task<bool> DeleteBasketAsync(string basketId)
    {
    }
}