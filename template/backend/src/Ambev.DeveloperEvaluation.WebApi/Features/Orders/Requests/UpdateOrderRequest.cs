using Ambev.DeveloperEvaluation.Application.Orders.Commands.UpdateOrder;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.Requests;

public record UpdateOrderRequest(Guid UserId, string Branch, UpdateOrderRequestItem[] Items)
{
    public UpdateOrderCommand ToCommand() =>
        new(
            UserId,
            Branch,
            Items.Select(i =>
                new UpdateOrderCommandItem(i.ProductId, i.Quantity)
            ).ToArray()
        );
}

public record UpdateOrderRequestItem(Guid ProductId, int Quantity);