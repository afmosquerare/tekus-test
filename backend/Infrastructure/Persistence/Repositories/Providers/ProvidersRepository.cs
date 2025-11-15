using Domain.Aggregates.Providers;
using Domain.Aggregates.Providers.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProvidersRepository(ApplicationDbContext context) : IProviderRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Provider> AddAsync(Provider provider)
    {
        await _context.Providers.AddAsync(provider);
        return provider;
    }
    public async Task<IEnumerable<Provider>> GetAllAsync()
    {
        return await _context.Providers
            .Include(p => p.Services)
            .Include(p => p.CustomFields)
            .ToListAsync();
    }

    public async Task<Provider?> GetByIdAsync(Guid id)
    {
        return await _context.Providers
            .Include(p => p.Services)
            .Include(p => p.CustomFields)
            .SingleOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Provider?> GetByEmailAsync(string email)
    {
        return await _context.Providers
            .Include(p => p.Services)
            .Include(p => p.CustomFields)
            .SingleOrDefaultAsync(p => p.Email.Value == email);
    }
    public async Task<Provider?> GetByNitAsync(string nit)
    {
        return await _context.Providers
            .Include(p => p.Services)
            .Include(p => p.CustomFields)
            .SingleOrDefaultAsync(p => p.Nit == nit);
    }

    public async Task RemoveAsync(Provider provider)
    {
        _context.Providers.Remove(provider);
    }

    public async Task<Provider> UpdateProviderAsync(Provider provider)
    {
        _context.Providers.Update(provider);
        return provider;
    }

    public async Task<bool> ExistsByNitAsync(string nit)
    {
        return await _context.Providers.AnyAsync(p => p.Nit == nit);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Providers.AnyAsync(p => p.Email == Email.Create(email ));
    }
}
