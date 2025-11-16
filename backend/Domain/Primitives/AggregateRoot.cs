namespace Domain.Primitives;

public interface IAggregateRoot { }

public abstract class AggregateRoot: Entity
{
    private readonly List<DomainEvent> _domainEvents = new();
    public ICollection<DomainEvent> GetDomainEvents() => _domainEvents;

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

}