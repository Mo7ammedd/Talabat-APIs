using Talabat.Core.Models;

namespace Talabat.Core.Services.Contract;

public interface IPaymentService
{
    
    Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
    
}