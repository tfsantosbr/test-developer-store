using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class OrderItemCanceledDomainEventHandler(ILogger<OrderItemCanceledDomainEventHandler> logger)
    : INotificationHandler<OrderItemCanceledDomainEvent>
{
    public Task Handle(OrderItemCanceledDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Item {ItemId} of order {OrderId} was canceled.", notification.ItemId, notification.OrderId);

        return Task.CompletedTask;
    }
}
