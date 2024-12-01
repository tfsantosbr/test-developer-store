using Ambev.DeveloperEvaluation.Common.Results;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Constants;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public sealed class Order : BaseEntity
{
    private readonly List<OrderItem> _items = [];
    private readonly List<Discount> _discounts = [];

    public Order(Guid userId, string branch)
    {
        Id = Guid.NewGuid();
        Number = OrderNumber.Create();
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
        Branch = branch;
    }

    public OrderNumber Number { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public Guid UserId { get; private set; }
    public string Branch { get; private set; } = string.Empty;
    public int Quantities => _items.Count;
    public decimal TotalWithDiscount => GetOrderTotalWithDiscount();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public IReadOnlyCollection<Discount> Discounts => _discounts.AsReadOnly();

    public void CalculateDiscount()
    {
        if (_items.Count >= 10 && _items.Count <= 20)
            _discounts.Add(new Discount(Id, 20));

        else if (_items.Count >= 4)
            _discounts.Add(new Discount(Id, 10));
    }

    public Result AddItem(Guid productId, int quantity)
    {
        if (OrderHasMoreThan20ItemsOfSameProduct(productId))
            return Result.Error(OrderErrors.OrderHasMoreThan20ItemsOfSameProduct(productId));

        _items.Add(new OrderItem(Id, productId, quantity));

        return Result.Success();
    }

    private bool OrderHasMoreThan20ItemsOfSameProduct(Guid productId)
    {
        return _items.Where(item => item.ProductId == productId)
            .Sum(item => item.Quantity) >= 20;
    }

    private decimal GetOrderTotalWithDiscount()
    {
        var total = _items.Sum(item => item.TotalPrice);
        var discount = _discounts.Sum(discount => total * discount.Percentage / 100);

        return total - discount;
    }
}
