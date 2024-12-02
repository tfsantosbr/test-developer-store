using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts").HasKey(o => o.Id);

        builder.Property(o => o.Id).ValueGeneratedNever();
        builder.Property(o => o.OrderId).ValueGeneratedNever();

        builder.HasOne<Order>().WithMany().HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Cascade);
    }
}
