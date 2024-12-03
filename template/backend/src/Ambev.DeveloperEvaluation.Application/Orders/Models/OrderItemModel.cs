using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Orders.Models;

public record OrderItemModel(Guid Id, string Branch, bool IsCanceled)
{
    public static OrderItemModel FromOrder(Order order) => 
        new(order.Id, order.Branch, order.IsCanceled);
}
