using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record OrderModifiedDomainEvent(Guid OrderId) : DomainEvent;

public class OrderModifiedDomainEventHandler(ILogger<OrderModifiedDomainEventHandler> logger)
    : INotificationHandler<OrderModifiedDomainEvent>
{
    public Task Handle(OrderModifiedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order {OrderId} was modified.", notification.OrderId);

        return Task.CompletedTask;
    }
}
