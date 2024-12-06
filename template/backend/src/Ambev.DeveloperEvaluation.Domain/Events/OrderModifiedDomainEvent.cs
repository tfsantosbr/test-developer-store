using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record OrderModifiedDomainEvent(Guid OrderId) : DomainEvent;
