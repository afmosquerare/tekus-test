using System.Text.Json.Serialization;
using Application.Commands.Providers.Dtos;
using Domain.Aggregates.Providers;
using Domain.Aggregates.Providers.ValueObjects;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Providers;

public record CreateCustomFieldCommand(
    CustomFieldDto customField
     ) : IRequest<ErrorOr<bool>>
{
    
    [JsonIgnore]
    public Guid ProviderId {get; init;}
};

internal sealed class CreateCustomFieldCommandHandle(IProviderRepository providerRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomFieldCommand, ErrorOr<bool>>
{
    private readonly IProviderRepository _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    public async Task<ErrorOr<bool>> Handle(CreateCustomFieldCommand req, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetByIdAsync( req.ProviderId );
        if( provider is null) return Error.NotFound($"provider doesn't exits with id {req.ProviderId} ");
        provider.AddCustomField( CustomField.Create( req.customField.FieldName, req.customField.FieldValue ) );
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken );
        return result > 0;
    }

}