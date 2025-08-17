using FluentValidation;

namespace Trench.Notification.Application.UseCases.Notifications.Commands.RegisterNotification;

internal class RegisterNotificationCommandValidator : AbstractValidator<RegisterNotificationCommand>
{
    public RegisterNotificationCommandValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum();

        RuleFor(x => x.RecipientUserId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.TriggeringUserId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}