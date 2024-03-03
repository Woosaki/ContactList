using ContactListAPI.Dtos;
using FluentValidation;

namespace ContactListAPI.Validators;

public class AddContactRequestValidator : AbstractValidator<AddContactRequest>
{
    public AddContactRequestValidator()
    {
        RuleFor(request => request.PhoneNumber)
            .Matches(@"^\d{9}$").WithMessage("Phone number must be a 9-digit number.");

        RuleFor(request => request.Birthdate)
            .LessThan(DateTime.Today).WithMessage("Birthdate must be a date from the past.");

        RuleFor(request => request.CategoryId)
            .InclusiveBetween(1, 3).WithMessage("Category ID must be 1, 2 or 3.");
    }
}
