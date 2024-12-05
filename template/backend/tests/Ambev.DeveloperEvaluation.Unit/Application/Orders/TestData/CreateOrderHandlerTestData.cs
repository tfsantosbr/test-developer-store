using Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Orders.TestData;

public static class CreateOrderHandlerTestData
{
    public static CreateOrderCommand GenerateValidCommand()
    {
        return new(Guid.NewGuid(), "Test Branch", [new(Guid.NewGuid(), 10)]);
    }

    public static CreateOrderCommand GenerateInvalidCommand()
    {
        var productId = Guid.NewGuid();

        return new(Guid.NewGuid(), "Test Branch", [new(productId, 20), new(productId, 1)]);
    }
}