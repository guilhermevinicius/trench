using FluentValidation;

namespace Trench.User.Application.UseCases.User.Commands.RegisterUser;

internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);

        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6);
    }
}