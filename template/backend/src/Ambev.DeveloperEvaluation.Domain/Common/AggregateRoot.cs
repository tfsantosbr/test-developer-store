namespace Ambev.DeveloperEvaluation.Domain.Common;

public abstract class AggregateRoot : BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void ClearEvents() =>
        _domainEvents.Clear();
}
