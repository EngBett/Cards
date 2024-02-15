using Cards.Application.Commands;
using FluentValidation;

namespace Cards.Application.Validations;

public class AddCardCommandValidator:AbstractValidator<AddCardCommand>
{
    public AddCardCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Color).Matches(@"^#([A-Fa-f0-9]{6})").When(x=>!string.IsNullOrWhiteSpace(x.Color)).WithMessage("Invalid color");
    }
}