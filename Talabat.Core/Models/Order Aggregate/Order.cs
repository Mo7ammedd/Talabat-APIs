using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.Core.Models.Order_Aggregate;

public class Order : BaseModel
{
    public Order(string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod,ICollection<OrderItem> orderItems,decimal subtotal)
    {
        BuyerEmail = buyerEmail;
        ShipToAddress = shipToAddress;
        DeliveryMethod = deliveryMethod;
        OrderItems = orderItems;
        Subtotal = subtotal;
    }
    public Order()
    {
        
    }
    public string BuyerEmail { get; set; }
    
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
    
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    
    public Address ShipToAddress { get; set; }
    
    public DeliveryMethod DeliveryMethod { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>(); //navigation property many to one
    
    public decimal Subtotal { get; set; }
    
    public decimal GetTotal()
        => Subtotal + DeliveryMethod.Cost;
    
    public string PaymentIntentId { get; set; } = "";
    
}