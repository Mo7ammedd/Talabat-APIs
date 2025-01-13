using System.Runtime.Serialization;

namespace Talabat.Core.Models.Order_Aggregate;

public enum OrderStatus
{
    
    //label for the order status
    [EnumMember(Value = "Pending")]
    Pending,
    
    [EnumMember(Value = "Payment Received")]
    paymentReceived,
    
    [EnumMember(Value = "Payment Failed")]
    PaymentFailed,
}