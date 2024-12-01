using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public sealed class Discount : BaseEntity
{
    public Discount(Guid orderId, int percentage)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Percentage = percentage;
    }

    private Discount()
    {
    }

    public Guid OrderId { get; private set; }
    public int Percentage { get; private set; }
}
