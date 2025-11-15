using Domain.Aggregates.Providers;
using Domain.Aggregates.Providers.ValueObjects;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Providers;


internal sealed class CreateProviderCommandHandle(IProviderRepository providerRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProviderCommand, ErrorOr<Guid>>
{

    private readonly IProviderRepository _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    public async Task<ErrorOr<Guid>> Handle(CreateProviderCommand req, CancellationToken cancellationToken)
    {
        if (Email.Create(req.Email) is not Email email)
        {
            return Error.Validation("Provider.Email", "Email format its not valid");
        }
        var provider = new Provider(req.Name, req.Nit, email);
        try
        {
            await _providerRepository.AddAsync(provider);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Error.Failure("CreateCustomer.Failure", ex.Message);
        }

        return provider.Id;
    }
}