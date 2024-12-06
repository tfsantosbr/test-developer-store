using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Orders.Models;

public record OrderDetailsModel(Guid Id, Guid UserId, string Branch, OrderItem[] Items, Discount[] Discounts, bool IsCanceled)
{
    public static OrderDetailsModel FromOrder(Order order)
    {
        var orderCreatedModel = new OrderDetailsModel(
            order.Id,
            order.UserId, 
            order.Branch, 
            order.Items.Select(item => new OrderItem(
                item.Id,
                item.ProductId, 
                item.Quantity, 
                item.UnitPrice, 
                item.TotalPrice)
            ).ToArray(), 
            order.Discounts.Select(discount => 
                new Discount(discount.Percentage)
            ).ToArray(),
            order.IsCanceled
        );

        return orderCreatedModel;
    }
}

public record OrderItem(Guid Id, Guid ProductId, int Quantity, decimal UnitPrice, decimal TotalPrice);

public record Discount(int Percentage);
