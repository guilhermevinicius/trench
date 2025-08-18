using FluentResults;
using Microsoft.Extensions.Localization;
using Trench.Notification.Application.Contracts.Caching;
using Trench.Notification.Application.Contracts.Messaging;
using Trench.Notification.Application.Contracts.Repositories;
using Trench.Notification.Domain.Aggregates.Notification.Dtos;
using Trench.Notification.Domain.Dtos;
using Trench.Notification.Domain.Extensions;
using Trench.Notification.Domain.Resources;

namespace Trench.Notification.Application.UseCases.Notifications.Queries.ListNotification;

internal sealed class ListNotificationQueryHandler(
    INotificationRepository notificationRepository,
    ICacheService cacheService,
    IStringLocalizer<DomainValidationResource> domainValidationResource) 
    : IQueryHandler<ListNotificationQuery, NotificationDto[]>
{
    public async Task<Result<NotificationDto[]>> Handle(ListNotificationQuery query,
        CancellationToken cancellationToken)
    {
        var notifications = await notificationRepository.ListNotificationsAsync(
            query.RecipientUserId,
            cancellationToken);

        if (notifications.Length == 0)
            return Result.Ok();

        List<NotificationDto> notificationsComplete = [];

        foreach (var notification in notifications)
        {
            var username = await cacheService.GetAsync<string>($"user_id:{notification.TriggeringUserId}", cancellationToken);
            var user = await cacheService.GetAsync<UserProfileDto>($"user:{username}", cancellationToken);

            notificationsComplete.Add(new NotificationDto(
                notification.Id,
                notification.Type,
                notification.IsRead,
                domainValidationResource[notification.Type.GetDescription()],
                user is not null ? new UserProfileDto(
                    user.Id,
                    user.PictureUrl,
                    user.FirstName,
                    user.LastName) : null,
                notification.CreatedOnUtc));
        }

        return notificationsComplete.ToArray();
    }
}