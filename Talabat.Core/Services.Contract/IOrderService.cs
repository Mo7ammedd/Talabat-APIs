using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Core.Services.Contract;

public interface IOrderService
{
    Task<Order?> CreateOrderAsync(string buyerEmail,  string basketId,int deliveryMethodId,  Address shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail);
    
}