namespace Talabat.Core.Models.Order_Aggregate;

public class DeliveryMethod : BaseModel
{
    public string ShortName { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public string DeliveryTime { get; set; }
}