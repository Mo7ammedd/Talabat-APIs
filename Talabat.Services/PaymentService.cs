using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Product = Talabat.Core.Models.Product;

namespace Talabat.Services;

public class PaymentService : IPaymentService
{
    private readonly IConfiguration _config;
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IConfiguration config,IBasketRepository basketRepository,IUnitOfWork unitOfWork)
    {
        _config = config;
        _basketRepository = basketRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
    {
        StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
        var basket = await _basketRepository.GetBasketAsync(basketId);
        if (basket == null) return null;
        

        var shippingPrice = 0m;
        if (basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Cost;
            shippingPrice = deliveryMethod.Cost;
        }
        if (basket.items.Count > 0)
        {
            foreach (var item in basket.items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                if (productItem.Price != item.Price)
                {
                    item.Price = productItem.Price;
                }
            }
        }
        PaymentIntentService service = new PaymentIntentService();
        PaymentIntent paymentIntent;

        if (basket.PaymentIntentId == null)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long) basket.items.Sum(i => i.Quantity * (i.Price * 100)) + (long) shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> {"card"}
            };
            paymentIntent = await service.CreateAsync(options);//create payment intent
            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long) basket.items.Sum(i => i.Quantity * (i.Price * 100)) + (long) shippingPrice * 100
            };
            await service.UpdateAsync(basket.PaymentIntentId, options);//update payment intent
        }
        await _basketRepository.UpdateBasketAsync(basket);
        
        
        return basket;
        
        
    }
}