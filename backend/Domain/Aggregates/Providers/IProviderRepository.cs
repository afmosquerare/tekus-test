using Domain.Aggregates.Providers.ValueObjects;

namespace Domain.Aggregates.Providers;

public interface IProviderRepository
{
    Task<Provider> AddAsync( Provider provider);
    Task<Provider?> GetByIdAsync( Guid id);
    Task<Provider?> GetByNitAsync( string nit);
    Task<Provider?> GetByEmailAsync( string email);
    Task<IEnumerable<Provider>> GetAllAsync();
    Task RemoveAsync(Provider provider);
    Task<bool> ExistsByNitAsync(string nit);
    Task<bool> ExistsByEmailAsync(string email);

}