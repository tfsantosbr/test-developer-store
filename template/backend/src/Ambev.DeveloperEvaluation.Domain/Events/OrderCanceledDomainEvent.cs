using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record OrderCanceledDomainEvent(Guid OrderId) : DomainEvent;
