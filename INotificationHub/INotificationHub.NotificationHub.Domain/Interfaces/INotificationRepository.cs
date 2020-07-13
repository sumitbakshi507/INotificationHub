using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace INotificationHub.NotificationHub.Domain.Interfaces
{
    public interface INotificationRepository
    {
        void SendEmail(string appKey, string subject, string body, string to, List<string> attachmentPath);
    }
}
