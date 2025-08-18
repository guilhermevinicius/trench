using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trench.Notification.Api.Configurations.Authentication;
using Trench.Notification.Application.UseCases.Notifications.Queries.ListNotification;

namespace Trench.Notification.Api.Controllers.V1;

[Route("api/v1/notifications")]
public class NotificationController(
    IUserContext userContext,
    ISender sender) 
    : TrenchNotificationBaseController
{
    [HttpGet]
    public async Task<IResult> ListNotifications(CancellationToken cancellationToken)
    {
        var query = new ListNotificationQuery(userContext.UserIdAsInt());

        var result = await sender.Send(query, cancellationToken);

        return CustomResponse(result, HttpStatusCode.OK);
    }
}