using System.Data;
using FluentValidation;

namespace Application.Commands.Providers.Validators;

public class CreateProviderCommandValidator : AbstractValidator<CreateProviderCommand>
{
    
    public CreateProviderCommandValidator()
    {
        RuleFor(r=> r.Name )
            .NotEmpty().MaximumLength(50);
        RuleFor( r => r.Email)
            .EmailAddress()
            .WithName("Email")
            .NotEmpty();
        RuleFor( r=> r.Nit ).NotEmpty().MaximumLength(9);
    }
}