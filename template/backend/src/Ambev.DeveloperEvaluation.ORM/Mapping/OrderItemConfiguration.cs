using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems").HasKey(o => new { o.OrderId, o.ProductId });

        builder.Property(o => o.OrderId).ValueGeneratedNever();
        builder.Property(o => o.ProductId).ValueGeneratedNever();

        builder.HasOne<Order>().WithMany().HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Product>().WithMany().HasForeignKey(o => o.ProductId).OnDelete(DeleteBehavior.Restrict);
    }
}
