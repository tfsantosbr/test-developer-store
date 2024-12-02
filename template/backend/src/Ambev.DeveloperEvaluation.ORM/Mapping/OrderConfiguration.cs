using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders").HasKey(o => o.Id);

        builder.Property(o => o.Id).ValueGeneratedNever();
        builder.Property(o => o.UserId).ValueGeneratedNever();
        builder.Property(o => o.CreatedAt).IsRequired();
        builder.Property(o => o.Branch).IsRequired().HasMaxLength(500);

        builder.ComplexProperty(o => o.Number, n =>
            n.Property(p => p.Value).HasColumnName("Number").IsRequired()
        );
        
        builder.HasOne<User>().WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(o => o.Items).WithOne().HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(o => o.Discounts).WithOne().HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(o => o.Quantities);
        builder.Ignore(o => o.Total);
        builder.Ignore(o => o.TotalWithDiscount);
    }
}
