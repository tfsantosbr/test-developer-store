using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record OrderItemCanceledDomainEvent(Guid OrderId, Guid ItemId) : DomainEvent;
