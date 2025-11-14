using Domain.Aggregates.Providers.ValueObjects;

namespace Domain.Aggregates.Providers;

public interface IProviderRepository
{
    Task<Guid> AddAsync( Provider provider);
    Task<Provider> GetByIdAsync( Guid id);
    Task<IEnumerable<Provider>> GetAllAsync();

    Task RemoveAsync(Provider provider);
    Task<Provider> UpdateProviderAsync(  Provider provider );

    Task AddCustomFieldAsync( CustomField customField);
}