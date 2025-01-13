using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Repository.Data.Config;

public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.OwnsOne(i => i.Product, p =>
        {
            p.WithOwner();
        });
        builder.Property(i => i.Price)
            .HasColumnType("decimal(18,2)");
    }
}