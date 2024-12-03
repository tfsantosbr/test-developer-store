using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public sealed class OrderItem : BaseEntity
{
    public OrderItem(Guid orderId, Product product, int quantity)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        ProductId = product.Id;
        UnitPrice = product.Price;
        Quantity = quantity;
    }

    private OrderItem()
    {
    }

    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice => UnitPrice * Quantity;
}
