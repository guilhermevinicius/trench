using MassTransit;
using MediatR;
using Trench.Notification.Application.UseCases.Notifications.Commands.RegisterNotification;
using Trench.Notification.MessageQueue.Messages;

namespace Trench.Notification.MessageQueue.Consumers;

public sealed class RegisterNotificationConsumer(
    ISender sender) 
    : IConsumer<IRegisterNotification>
{
    public async Task Consume(ConsumeContext<IRegisterNotification> context)
    {
        try
        {
            var message = context.Message;
            await sender.Send(new RegisterNotificationCommand(
                message.Type,
                message.RecipientUserId,
                message.TriggeringUserId));
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}