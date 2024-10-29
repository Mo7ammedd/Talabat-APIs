using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IBasketRepository _basketRepo;

    public OrderService(IBasketRepository basketRepo,
        IUnitOfWork unitOfWork)

    {
        _unitOfWork = unitOfWork;
        _basketRepo = basketRepo;
    }
    
    public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
    {
        // get basket from the repo
        var basket = await _basketRepo.GetBasketAsync(basketId);
        // get items from the product repo
        var orderItems = new List<OrderItem>();
        if (basket?.items != null)
        {
            foreach (var item in basket.items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                var productItemOrdered = new ProductItemOrder(productItem.Id, productItem.Name, productItem.PictureUrl); 
                var itemOrdered = new OrderItem(productItemOrdered, productItem.Price, item.Quantity);
                orderItems.Add(itemOrdered);
          
            }
        }
        // calculate subtotal
        var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
        
        // get delivery method
        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
        
        // create order
        var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal);
        
        // save to db

        _unitOfWork.Repository<Order>().AddAsync(order);
        
        var result = await _unitOfWork.CompleteAsync();
            
        if (result <= 0) return null;
        
        return order;
        
        
        
    }

    public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
    {
        throw new NotImplementedException();
    }
}