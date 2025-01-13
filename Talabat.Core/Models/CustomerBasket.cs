namespace Talabat.Core.Models;

public class CustomerBasket
{
    public CustomerBasket(string id)
    {
        this.id = id;
        items = new List<BasketItem>();
    }

    public string id { get; set; }
    public List<BasketItem> items { get; set; } 
    
    public string PaymentIntentId { get; set; }

    public string ClientSecret { get; set; }
    
    public int? DeliveryMethodId { get; set; }
    
    public decimal ShippingPrice { get; set; }
}

