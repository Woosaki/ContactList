using ContactListAPI.Dtos;
using FluentValidation;

namespace ContactListAPI.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.Email)
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(request => request.Password)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(request => request.ConfirmPassword)
            .Equal(request => request.Password).WithMessage("Password and confirm password must match.");
    }
}
