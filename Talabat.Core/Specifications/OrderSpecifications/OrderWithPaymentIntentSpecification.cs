using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Core.Specification.OrderSpecifications;

public class OrderWithPaymentIntentSpecification : BaseSpecifications<Order>
{
    public OrderWithPaymentIntentSpecification(string paymentIntentId):base((o=>o.PaymentIntentId==paymentIntentId))
    {
        
    }
}  