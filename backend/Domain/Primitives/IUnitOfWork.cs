namespace Domain.Primitives;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken c = default);
}