using System.Data;
using FluentValidation;

namespace Application.Commands.Providers.Validators;

public class UpdateProviderCommandValidator : AbstractValidator<UpdateProviderCommand>
{
    
    public UpdateProviderCommandValidator()
    {
        RuleFor(r=> r.Name )
            .NotEmpty()
            .MaximumLength(50);

        RuleFor( r => r.Email)
            .EmailAddress()
            .WithName("Email")
            .NotEmpty();

        RuleFor( r=> r.Nit ).NotEmpty().MaximumLength(9);
    }
}