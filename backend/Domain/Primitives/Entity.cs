namespace Domain.Primitives;

public abstract class Entity
{
   public Guid Id { get; protected set; } 

}

public abstract class Entity<T>
{
   public T Id { get; protected set; } 

}