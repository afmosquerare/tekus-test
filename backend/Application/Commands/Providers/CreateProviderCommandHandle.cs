using Domain.Aggregates.Providers;
using Domain.Aggregates.Providers.ValueObjects;
using Domain.Primitives;
using MediatR;

namespace Application.Commands.Providers;


internal sealed class CreateProviderCommandHandle( IProviderRepository providerRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProviderCommand, Guid>
{

    private readonly IProviderRepository _providerRepository = providerRepository ?? throw new ArgumentException( nameof(providerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentException( nameof( unitOfWork) );
    public async Task<Guid> Handle(CreateProviderCommand req, CancellationToken cancellationToken)
    {
        if( Email.Create(req.Email) is not Email email)
        {
            throw new ArgumentException(nameof( email ));
        }

        var provider = new Provider( req.Name, req.Nit, email);
        var guid =  await _providerRepository.AddAsync( provider );
        await _unitOfWork.SaveChangesAsync( cancellationToken );
        return guid;
    }
}