using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record OrderCreatedDomainEvent(Guid OrderId) : DomainEvent;
