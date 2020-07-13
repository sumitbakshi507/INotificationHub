using INotificationHub.Domain.Core.Bus;
using INotificationHub.NotificationHub.Domain.Events;
using INotificationHub.NotificationHub.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace INotificationHub.NotificationHub.Domain.EventHandlers
{
    public class NotificationCreatedEventHandler : IEventHandler<NotificationCreatedEvent>
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationCreatedEventHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public Task Handle(NotificationCreatedEvent @event)
        {
            try
            {
                Log.Information("NotificationCreatedEvent", @event);
                _notificationRepository.SendEmail(@event.AppKey, @event.Subject, @event.Body, @event.To, @event.AttachmentPath);
                if (!String.IsNullOrEmpty(@event.AcknowledgementMessage)) {
                }
            }
            catch (Exception ex) {
                Log.Error(ex, "NotificationCreatedEvent");
            }

            return Task.CompletedTask;
        }
    }
}
