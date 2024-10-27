namespace Talabat.Core.Models;

public class CustomerBasket
{
    public CustomerBasket(string id)
    {
        this.id = id;
        items = new List<BasketItem>();
    }

    public string id { get; set; }
    public List<BasketItem> items { get; set; } = new List<BasketItem>();
}

