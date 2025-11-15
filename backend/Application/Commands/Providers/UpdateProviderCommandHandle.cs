using Application.Commands.Providers.Dtos;
using Domain.Aggregates.Providers;
using Domain.Aggregates.Providers.ValueObjects;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Providers;

public record UpdateProviderCommand(
    Guid Id,
    string? Name,
    string? Email,
    string? Nit,
    List<CustomFieldDto>? CustomFields,
    List<ServicesDto>? Services
) : IRequest<ErrorOr<Guid>>;

internal sealed class UpdateProviderCommandHandler(
    IProviderRepository providerRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateProviderCommand, ErrorOr<Guid>>
{
    private readonly IProviderRepository _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<Guid>> Handle(UpdateProviderCommand req, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetByIdAsync(req.Id);
        if (provider is null)
            return Error.NotFound("Provider.Id", $"Provider with id {req.Id} not found");

        if (req.Email is not null) provider.UpdateEmail(req.Email);
        if (req.Name is not null) provider.UpdateName(req.Name);
        if (req.Nit is not null) provider.UpdateNit(req.Nit);

        if (req.CustomFields is not null)
        {
            var toRemove = provider.CustomFields
                .Where(cf => !req.CustomFields.Any(dto => dto.FieldName == cf.FieldName))
                .ToList();
            foreach (var cf in toRemove)
                provider.RemoveCustomField(cf);

            foreach (var dto in req.CustomFields)
            {
                var existing = provider.CustomFields.FirstOrDefault(cf => cf.FieldName == dto.FieldName);
                if (existing is null)
                {
                    provider.AddCustomField(dto.FieldName, dto.FieldValue);
                }
                else if (existing.FieldValue != dto.FieldValue)
                {
                    provider.RemoveCustomField(existing);
                    provider.AddCustomField(dto.FieldName, dto.FieldValue);
                }
            }
        }

        if (req.Services is not null)


        {
            var toRemoveServices = provider.Services
                .Where(s => !req.Services.Any(s => s.Id == s.Id))
                .ToList();
            foreach (var s in toRemoveServices)
                provider.RemoveService(s);

            foreach (var service in req.Services)
            {
                var existing = provider.Services.FirstOrDefault(s => s.Id == service.Id);
                if (existing is null)
                {
                    var result = new List<Country>();
                    foreach (var c in service.Countries)
                    {
                        result.Add( Country.Create( c.Name, c.Code ) );
                    }
                    provider.AddService(service.Name, service.HourlyRate, result);
                }
                else
                {
                    provider.UpdateService( existing.Id, existing.Name, existing.HourlyRate );
                    var toRemoveCountries = existing.Countries
                        .Where(c => !service.Countries.Any(dc => dc.Code == c.Code))
                        .ToList();
                    foreach (var c in toRemoveCountries)
                        provider.RemoveCountryFromService( existing.Id, c.Code);

                    foreach (var country in service.Countries)
                    {
                        if (!existing.Countries.Any(c => c.Code == country.Code))
                            provider.AddCountryToService( existing.Id, Country.Create( country.Name, country.Code  ) );
                    }
                }
            }
        }
        
        await _providerRepository.UpdateProviderAsync( provider );
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return provider.Id;
    }

}
