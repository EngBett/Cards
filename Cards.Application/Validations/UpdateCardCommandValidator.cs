using Cards.Application.Commands;
using FluentValidation;

namespace Cards.Application.Validations;

public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
{
    public UpdateCardCommandValidator()
    {
        RuleFor(x => x.CardId)
            .NotEmpty()
            .WithMessage("CardId is required");
        
        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Status is required")
            .IsInEnum().WithMessage("Invalid Status");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Color)
            .Matches(@"^#([A-Fa-f0-9]{6})")
            .When(x => !string.IsNullOrWhiteSpace(x.Color))
            .WithMessage("Invalid color");
    }
}