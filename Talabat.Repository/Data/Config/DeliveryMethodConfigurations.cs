using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Repository.Data.Config;

public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(d => d.Cost)
            .HasColumnType("decimal(18,2)");
    }
}