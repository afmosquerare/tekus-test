using Domain.Aggregates.Providers.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Commands.Providers;


public record CreateProviderCommand( string Name, string Email, string Nit, List<CustomField>? CustomFields ) : IRequest<ErrorOr<Guid>>;
