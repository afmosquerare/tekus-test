using Application.Commands.Providers.Dtos;
using Domain.Aggregates.Providers;
using Domain.Aggregates.Providers.ValueObjects;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Providers;

public record RemoveCustomFieldCommand(
    Guid Id,
    string FieldName
     ) : IRequest<ErrorOr<bool>>;

internal sealed class RemoveCustomFieldCommandHandle(IProviderRepository providerRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveCustomFieldCommand, ErrorOr<bool>>
{
    private readonly IProviderRepository _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    public async Task<ErrorOr<bool>> Handle(RemoveCustomFieldCommand req, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetByIdAsync(req.Id);
        if (provider is null)
            return Error.NotFound("Provider.Id", $"Provider with id {req.Id} not found");
        provider.RemoveCustomFieldByName( req.FieldName  );
        var results = await _unitOfWork.SaveChangesAsync( cancellationToken );
        return results > 0;
    }

}