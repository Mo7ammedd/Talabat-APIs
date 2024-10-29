namespace Talabat.Core.Models.Order_Aggregate;

public class Order : BaseModel
{
    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public Address ShipToAddress { get; set; }
    
}