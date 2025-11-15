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
        await _context.SaveChangesAsync();
        return provider;
    }

    public async Task AddCustomFieldAsync(Guid providerId, CustomField customField)
    {
        var provider = await _context.Providers
            .Include(p => p.CustomFields)
            .SingleOrDefaultAsync(p => p.Id == providerId);

        if (provider is null)
            throw new ArgumentException($"Provider with id {providerId} not found");

        provider.AddCustomField(customField.FieldName, customField.FieldValue, customField.FieldType);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Provider>> GetAllAsync()
    {
        return await _context.Providers
            .Include(p => p.Services)
            .Include(p => p.CustomFields)
            .ToListAsync();
    }

    public async Task<Provider> GetByIdAsync(Guid id)
    {
        var provider = await _context.Providers
            .Include(p => p.Services)
            .Include(p => p.CustomFields)
            .SingleOrDefaultAsync(p => p.Id == id);  

        if (provider is null)
            throw new ArgumentException($"Provider with id {id} not found");

        return provider;
    }

    public async Task RemoveAsync(Provider provider)
    {
        _context.Providers.Remove(provider);
        await _context.SaveChangesAsync();
    }

    public async Task<Provider> UpdateProviderAsync(Provider provider)
    {
        _context.Providers.Update(provider);
        await _context.SaveChangesAsync();
        return provider;
    }
}
