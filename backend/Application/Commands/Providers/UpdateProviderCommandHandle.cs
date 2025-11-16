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
    string? Nit
) : IRequest<ErrorOr<bool>>;

internal sealed class UpdateProviderCommandHandler(
    IProviderRepository providerRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateProviderCommand, ErrorOr<bool>>
{
    private readonly IProviderRepository _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateProviderCommand req, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetByIdAsync(req.Id);
        if (provider is null)
            return Error.NotFound("Provider.Id", $"Provider with id {req.Id} not found");

        if (req.Email is not null) provider.UpdateEmail(req.Email);
        if (req.Name is not null) provider.UpdateName(req.Name);
        if (req.Nit is not null) provider.UpdateNit(req.Nit);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
