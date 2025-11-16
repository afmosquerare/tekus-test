using System.Text.Json.Serialization;
using Application.Commands.Providers.Dtos;
using Domain.Aggregates.Providers;
using Domain.Aggregates.Providers.ValueObjects;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Providers;

public record CreateServiceCommand(
    decimal HourlyRate,
    string Name
     ) : IRequest<ErrorOr<int>>
{
    [JsonIgnore]
    public Guid ProviderId { get; init; }
};

internal sealed class CreateServiceCommandHandle(IProviderRepository providerRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateServiceCommand, ErrorOr<int>>
{
    private readonly IProviderRepository _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    public async Task<ErrorOr<int>> Handle(CreateServiceCommand req, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetByIdAsync(req.ProviderId);
        if (provider is null)
            return Error.NotFound("Provider.Id", $"Provider with id {req.ProviderId} not found");
        if( provider.HasServiceByName( req.Name ) ) return Error.NotFound("Provider.Service", $"Provider with service name {req.Name} already exits");
        provider.AddService( req.Name, req.HourlyRate  );
        var results = await _unitOfWork.SaveChangesAsync( cancellationToken );
        return results > 0 ? provider.Services.Last().Id : 0;
    }

}