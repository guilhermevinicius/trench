using FluentValidation;

namespace Trench.Notification.Application.UseCases.Notifications.Queries.ListNotification;

internal class ListNotificationQueryValidator : AbstractValidator<ListNotificationQuery>
{
    public ListNotificationQueryValidator()
    {
        RuleFor(x => x.RecipientUserId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}