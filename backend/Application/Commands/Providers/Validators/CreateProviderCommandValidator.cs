using FluentValidation;

namespace Application.Commands.Providers.Validators;

public class CreateProviderCommandValidator : AbstractValidator<CreateProviderCommand>
{
    
    public CreateProviderCommandValidator()
    {
        RuleFor(r=> r.Name )
            .NotEmpty().MaximumLength(50);
    }
}