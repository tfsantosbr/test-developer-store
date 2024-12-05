using Ambev.DeveloperEvaluation.Common.Results;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Constants;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public sealed class Order : AggregateRoot
{
    // Fields

    private readonly List<OrderItem> _items = [];
    private readonly List<Discount> _discounts = [];

    // Constructors

    private Order(User user, string branch)
    {
        Id = Guid.NewGuid();
        Number = OrderNumber.Create();
        CreatedAt = DateTime.UtcNow;
        UserId = user.Id;
        Branch = branch;

        RaiseEvent(new OrderCreatedDomainEvent(Id));
    }

    private Order()
    {
    }

    // Properties

    public OrderNumber Number { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public Guid UserId { get; private set; }
    public string Branch { get; private set; } = string.Empty;
    public bool IsCanceled { get; private set; }
    public int Quantities => GetItemQuantities();
    public decimal Total => GetOrderTotal();
    public decimal TotalWithDiscount => GetOrderTotalWithDiscount();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public IReadOnlyCollection<Discount> Discounts => _discounts.AsReadOnly();

    // Public Methods

    public static Order Create(User user, string branch)
    {
        var order = new Order(user,branch);

        return order;
    }

    public void Update(string branch)
    {
        Branch = branch;

        RaiseEvent(new OrderModifiedDomainEvent(Id));
    }

    public void Cancel()
    {
        IsCanceled = true;

        RaiseEvent(new OrderCanceledDomainEvent(Id));
    }

    public void CalculateDiscount()
    {
        var itemQuantities = GetItemQuantities();

        if (itemQuantities >= 10 && itemQuantities <= 20)
            _discounts.Add(new Discount(Id, 20));

        else if (itemQuantities >= 4)
            _discounts.Add(new Discount(Id, 10));
    }

    public Result AddItem(Product product, int quantity)
    {
        if (quantity <= 0)
            return Result.Error(OrderErrors.CantAddItemWithQuantityLessThanZero());

        if (OrderHasMoreThan20ItemsOfSameProduct(product.Id))
            return Result.Error(OrderErrors.OrderHasMoreThan20ItemsOfSameProduct(product.Id));

        _items.Add(new OrderItem(Id, product, quantity));

        return Result.Success();
    }

    public Result CancelItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(item => item.Id == itemId);

        if (item is null)
            return Result.Error(OrderErrors.ItemNotFound(itemId));

        _items.Remove(item);

        RaiseEvent(new OrderItemCanceledDomainEvent(Id, itemId));

        return Result.Success();
    }

    public void ClearItems() => _items.Clear();

    // Private Methods

    private int GetItemQuantities()
    {
        return _items.Sum(item => item.Quantity);
    }

    private bool OrderHasMoreThan20ItemsOfSameProduct(Guid productId)
    {
        return _items.Where(item => item.ProductId == productId)
            .Sum(item => item.Quantity) >= 20;
    }

    private decimal GetOrderTotal()
    {
        var total = _items.Sum(item => item.TotalPrice);

        return total;
    }

    private decimal GetOrderTotalWithDiscount()
    {
        var total = GetOrderTotal();
        var discount = _discounts.Sum(discount => total * discount.Percentage / 100);

        return total - discount;
    }
}
