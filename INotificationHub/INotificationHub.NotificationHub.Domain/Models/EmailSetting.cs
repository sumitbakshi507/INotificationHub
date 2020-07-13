using System;
using System.Collections.Generic;
using System.Text;

namespace INotificationHub.NotificationHub.Domain.Models
{
    public class EmailSetting
    {
        public String PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; }

        public String UsernameEmail { get; set; }

        public String UsernamePassword { get; set; }

        public String FromEmail { get; set; }

        public String ToEmail { get; set; }

        public String CcEmail { get; set; }

        public bool IsFake { get; set; }
    }
}
