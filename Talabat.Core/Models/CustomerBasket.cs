namespace Talabat.Core.Models;

public class CustomerBasket
{
    public string id { get; set; }
    public List<BasketItem> items { get; set; } = new List<BasketItem>();
}

