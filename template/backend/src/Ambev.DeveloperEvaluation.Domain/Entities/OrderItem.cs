namespace Ambev.DeveloperEvaluation.Domain.Entities;

public sealed class OrderItem
{
    public OrderItem(Guid orderId, Guid productId, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    private OrderItem()
    {
    }

    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Product Product { get; private set; } = default!;
    public decimal TotalPrice => Product.Price * Quantity;
}
