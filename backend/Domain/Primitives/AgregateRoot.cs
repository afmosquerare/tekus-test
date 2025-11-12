namespace Domain.Primitives;

public abstract class AgregateRoot
{
    private readonly List<DomainEvent> _domainEvents = new();
    public ICollection<DomainEvent> GetDomainEvents() => _domainEvens;

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

}