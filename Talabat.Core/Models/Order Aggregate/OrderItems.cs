namespace Talabat.Core.Models.Order_Aggregate;

public class OrderItems : BaseModel
{
    
    public ProductItemOrder Product { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
}