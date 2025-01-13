namespace Talabat.Core.Models.Order_Aggregate;

public class DeliveryMethod : BaseModel
{
    public DeliveryMethod(string shortName, string description, decimal cost, string deliveryTime)
    {
        ShortName = shortName;
        Description = description;
        Cost = cost;
        DeliveryTime = deliveryTime;
    }

    public DeliveryMethod()
    {
        
    }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public string DeliveryTime { get; set; }
}