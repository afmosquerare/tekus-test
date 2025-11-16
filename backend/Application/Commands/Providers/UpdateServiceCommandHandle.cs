using System.Text.Json.Serialization;
using Application.Commands.Providers.Dtos;
using Domain.Aggregates.Providers;
using Domain.Aggregates.Providers.ValueObjects;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Providers;

public record UpdateServiceCommand(
    decimal HourlyRate,
    string Name
     ) : IRequest<ErrorOr<bool>>
{
    [JsonIgnore]
    public Guid ProviderId { get; init; }

    [JsonIgnore]
    public int ServiceId { get; init; }
};

internal sealed class UpdateServiceCommandHandle(IProviderRepository providerRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateServiceCommand, ErrorOr<bool>>
{
    private readonly IProviderRepository _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    public async Task<ErrorOr<bool>> Handle(UpdateServiceCommand req, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetByIdAsync(req.ProviderId);
        if (provider is null)
            return Error.NotFound("Provider.Id", $"Provider with id {req.ProviderId} not found");

        if (provider is null)
            return Error.NotFound("Provider.Service.Id", $"Service with id {req.ServiceId} not found");
        provider.UpdateService( req.ServiceId, req.Name, req.HourlyRate );
        var results = await _unitOfWork.SaveChangesAsync( cancellationToken );
        return results > 0;
    }

}