using Application.Commands.Providers.Dtos;
using Domain.Aggregates.Providers;
using Domain.Aggregates.Providers.ValueObjects;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Providers;

public record CreateProviderCommand(
    string Name,
    string Email,
    string Nit,
    List<CustomFieldDto>? CustomFields,
    List<ServicesDto>? Services
     ) : IRequest<ErrorOr<Guid>>;

internal sealed class CreateProviderCommandHandle(IProviderRepository providerRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProviderCommand, ErrorOr<Guid>>
{
    private readonly IProviderRepository _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    public async Task<ErrorOr<Guid>> Handle(CreateProviderCommand req, CancellationToken cancellationToken)
    {
        var email = Email.Create(req.Email);

        var exits = await _providerRepository.ExistsByNitAsync(req.Nit);
        if (exits)
            return Error.Conflict("Provider.Nit", $"Provider already exits with nit {req.Nit}");

        exits = await _providerRepository.ExistsByEmailAsync(req.Email);
        if (exits)
            return Error.Conflict("Provider.Email", $"Provider already exits with email {req.Email}");

        var provider = new Provider(req.Name, req.Nit, email);
        AddCustomFields(provider, req.CustomFields);
        AddServices(provider, req.Services);
        try
        {
            await _providerRepository.AddAsync(provider);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Error.Failure("CreateProvider.Failure", ex.Message);
        }

        return provider.Id;
    }

    private void AddServices(Provider provider, List<ServicesDto>? services)
    {
        if (services is null || services.Count == 0 ) return;
        foreach (var s in services)
        {
            var countries = s.Countries?
                .Select(c => Country.Create(c.Name, c.Code))
                .Where(c => c is not null)
                .Cast<Country>()
                .ToList() ?? new List<Country>();


            provider.AddService(s.Name, s.HourlyRate, countries);
        }
    }

    private void AddCustomFields(Provider provider, List<CustomFieldDto>? customFields)
    {
        if (customFields is null || customFields.Count == 0 ) return;
        var fieldsToAdd = customFields?
            .Select(cf => CustomField.Create(cf.FieldName, cf.FieldValue))
            .Where(cf => cf is not null)
            .Cast<CustomField>()
            .ToList() ?? new List<CustomField>();

        provider.AddCustomsFields(fieldsToAdd);
    }
}