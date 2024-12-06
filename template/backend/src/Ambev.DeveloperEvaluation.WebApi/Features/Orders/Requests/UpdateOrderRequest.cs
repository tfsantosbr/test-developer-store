using Ambev.DeveloperEvaluation.Application.Orders.Commands.UpdateOrder;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.Requests;

public record UpdateOrderRequest(string Branch, UpdateOrderRequestItem[] Items)
{
    public UpdateOrderCommand ToCommand(Guid orderId) =>
        new(
            orderId,
            Branch,
            Items.Select(i =>
                new UpdateOrderCommandItem(i.ProductId, i.Quantity)
            ).ToArray()
        );
}

public record UpdateOrderRequestItem(Guid ProductId, int Quantity);