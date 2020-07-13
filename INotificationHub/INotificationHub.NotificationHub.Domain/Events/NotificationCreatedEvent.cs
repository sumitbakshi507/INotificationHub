using INotificationHub.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace INotificationHub.NotificationHub.Domain.Events
{
    public class NotificationCreatedEvent: Event
    {
        public string Subject { get; private set; }

        public string Body { get; private set; }

        public string To { get; private set; }

        public string AppKey { get; private set; }

        public List<string> AttachmentPath { get; private set; }

        public string AcknowledgementMessage { get; private set; }

        public NotificationCreatedEvent(
            string subject,
            string body, 
            string to, 
            string appKey,
            List<string> attachmentPath,
            string acknowledgementMessage)
        {
            Subject = subject;
            Body = body;
            To = to;
            AppKey = appKey;
            AttachmentPath = (attachmentPath == null) ? new List<string>() : attachmentPath;
            AcknowledgementMessage = acknowledgementMessage;
        }
    }
}
