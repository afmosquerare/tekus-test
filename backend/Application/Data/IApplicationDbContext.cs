using Domain.Providers;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Provider> Providers { get; set; }

    Task<int> SaveChangeAsync( CancellationToken c = default );
}