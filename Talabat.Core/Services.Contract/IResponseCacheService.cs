namespace Talabat.Core.Services.Contract;

public interface IResponseCacheService
{
    Task CacheResponseAsync(string key, object response, TimeSpan timeToLife);
    Task<string> GetCacheResponseAsync(string cacheKey);
}