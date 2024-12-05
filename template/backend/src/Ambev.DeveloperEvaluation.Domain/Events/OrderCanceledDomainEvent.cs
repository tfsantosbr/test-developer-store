using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record OrderCanceledDomainEvent(Guid OrderId) : DomainEvent;

public class OrderCanceledDomainEventHandler(ILogger<OrderCanceledDomainEventHandler> logger) 
    : INotificationHandler<OrderCanceledDomainEvent>
{
    public Task Handle(OrderCanceledDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order {OrderId} was canceled.", notification.OrderId);

        return Task.CompletedTask;
    }
}
