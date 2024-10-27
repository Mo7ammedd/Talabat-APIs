using System.Text.Json;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;
using StackExchange.Redis;

namespace Talabat.Repository;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;
    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
   
    public async Task<CustomerBasket> GetBasketAsync(string basketId)
    {
        var data = await _database.StringGetAsync(basketId);
        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
    }

    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
    {
        var createdOrUpdated = await _database.StringSetAsync(basket.id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
        if (!createdOrUpdated) return null;
        return await GetBasketAsync(basket.id);
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        return await _database.KeyDeleteAsync(basketId);
    }
}