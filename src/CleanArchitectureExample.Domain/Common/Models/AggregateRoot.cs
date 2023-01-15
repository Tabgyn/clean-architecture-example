using System.Diagnostics.CodeAnalysis;
using CleanArchitectureExample.Domain.Common.Interfaces;

namespace CleanArchitectureExample.Domain.Common.Models;

[ExcludeFromCodeCoverage]
public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents;

    protected AggregateRoot(TId id)
        : base(id)
    {
        _domainEvents = new List<IDomainEvent>();
    }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
