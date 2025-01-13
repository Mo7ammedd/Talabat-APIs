using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Core.Specification.OrderSpecifications;

public class OrderSpecifications : BaseSpecifications<Order>
{
    public OrderSpecifications(string buyerEmail):base(o=>o.BuyerEmail==buyerEmail)
    {
            Includes.Add(o=>o.DeliveryMethod);
            Includes.Add(o=>o.OrderItems);
            AddOrderByDesc(o=>o.OrderDate);
            
            // ApplyPagination();
            
    }
    public OrderSpecifications(int orderId, string buyerEmail):base(o=>o.Id==orderId && o.BuyerEmail==buyerEmail)
    {
            Includes.Add(o=>o.DeliveryMethod);
            Includes.Add(o=>o.OrderItems);
    }
    
   
} 