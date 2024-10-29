namespace Talabat.Core.Models.Order_Aggregate;

public class OrderItems : BaseModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
}