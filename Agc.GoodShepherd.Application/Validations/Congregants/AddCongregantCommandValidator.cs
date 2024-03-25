using Agc.GoodShepherd.Application.Commands.Congregants;
using Agc.GoodShepherd.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PhoneNumbers;

namespace Agc.GoodShepherd.Application.Validations.Congregants;

public class AddCongregantCommandValidator : AbstractValidator<AddCongregantCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly PhoneNumberUtil _phoneNumberUtil;

    public AddCongregantCommandValidator(IAppDbContext dbContext, PhoneNumberUtil phoneNumberUtil)
    {
        _dbContext = dbContext;
        _phoneNumberUtil = phoneNumberUtil;
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid Email Address")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Must(ValidPhoneNumber).WithMessage("Invalid Phone number").When(x=>!string.IsNullOrWhiteSpace(x.PhoneNumber));
    }

    private bool ValidPhoneNumber(string phoneNumber)
    {
        try
        {
            _phoneNumberUtil.Parse(phoneNumber, null);
            if (phoneNumber.Length > 13) return false;
            return true;
        }
        catch (NumberParseException ex)
        {
            return false;
        }
    }
}