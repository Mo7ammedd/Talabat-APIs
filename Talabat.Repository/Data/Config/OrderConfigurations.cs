using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Models.Order_Aggregate;
using Order = Talabat.Core.Models.Order_Aggregate.Order;

namespace Talabat.Repository.Data.Config;

internal class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(o => o.ShipToAddress, a =>
        {
            a.WithOwner(); // 1 to 1 relationship 
        });
        builder.Property(o=> o.Status)
            .HasConversion(
                o => o.ToString(),
                o => (OrderStatus) Enum.Parse(typeof(OrderStatus), o)
            );
      builder.Property(o=>o.Subtotal)
          .HasColumnType("decimal(18,2)");
      builder.HasOne(o=>o.DeliveryMethod)
          .WithMany()
          .OnDelete(DeleteBehavior.SetNull);    
    }
}