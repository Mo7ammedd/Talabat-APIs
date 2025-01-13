namespace Talabat.Core.Models.Order_Aggregate;

public class OrderItem : BaseModel
{
    public OrderItem(ProductItemOrder product, decimal price, int quantity)
    {
        Product = product;
        Price = price;
        Quantity = quantity;
    }

    public OrderItem()
    {
        
    }
    
    public ProductItemOrder Product { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
}