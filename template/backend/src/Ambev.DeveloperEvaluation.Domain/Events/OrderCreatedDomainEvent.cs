using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record OrderCreatedDomainEvent(Guid OrderId) : DomainEvent;

public class OrderCreatedDomainEventHandler(ILogger<OrderCreatedDomainEventHandler> logger)
    : INotificationHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order {OrderId} was created.", notification.OrderId);

        return Task.CompletedTask;
    }
}
