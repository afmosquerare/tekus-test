using System.Data;
using FluentValidation;

namespace Application.Commands.Providers.Validators;

public class RemoveCustomFieldCommandValidator : AbstractValidator<RemoveCustomFieldCommand>
{
    
    public RemoveCustomFieldCommandValidator()
    {
        RuleFor(r=> r.FieldName ).NotEmpty();

        RuleFor( r => r.Id).NotEmpty();

    }
}