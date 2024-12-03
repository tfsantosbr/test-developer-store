using Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Orders.TestData;

public static class CreateOrderHandlerTestData
{
    private static readonly Faker<CreateOrderCommand> createOrderHandlerFaker = new Faker<CreateOrderCommand>()
        .RuleFor(o => o.UserId, f => Guid.NewGuid())
        .RuleFor(o => o.Branch, f => f.Company.CompanyName())
        .RuleFor(o => o.Items, f => [new(Guid.NewGuid(), f.Random.Int(1, 10))]);

    public static CreateOrderCommand GenerateValidCommand()
    {
        return createOrderHandlerFaker.Generate();
    }
}