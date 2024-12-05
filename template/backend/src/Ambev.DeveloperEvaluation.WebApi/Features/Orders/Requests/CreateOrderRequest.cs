using Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.Requests;

public record CreateOrderRequest(Guid UserId, string Branch, CreateOrderRequestItem[] Items)
{
    public CreateOrderCommand ToCommand() =>
        new(
            UserId,
            Branch,
            Items.Select(i => 
                new CreateOrderCommandItem(i.ProductId, i.Quantity)
            ).ToArray()
        );
}

public record CreateOrderRequestItem(Guid ProductId, int Quantity);

