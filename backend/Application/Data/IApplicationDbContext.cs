using Domain.Aggregates.Providers;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    public DbSet<Provider> Providers { get; set; }
    Task<int> SaveChangesAsync( CancellationToken cancellationToken = default);
}