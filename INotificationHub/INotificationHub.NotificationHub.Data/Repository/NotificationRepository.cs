using INotificationHub.NotificationHub.Domain.Interfaces;
using INotificationHub.NotificationHub.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace INotificationHub.NotificationHub.Data.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public EmailSetting _emailSettings { get; }

        private readonly ILogger<NotificationRepository> _logger;

        public NotificationRepository(
            ILogger<NotificationRepository> logger,
            IOptions<EmailSetting> emailSettings)
        {
            _logger = logger;
            _emailSettings = emailSettings.Value;
            _logger.LogInformation("NotificationRepository Initiated");
        }

        public void SendEmail(string appKey, string subject, string body, string to, List<string> attachmentPath)
        {
            if (ValidateKey(appKey)) {
                try
                {
                    Execute(appKey, to, subject, body, attachmentPath).Wait();

                    Log.Information("email was sent successfully!");
                }
                catch (Exception ep)
                {
                    Log.Error(ep, "failed to send email");
                }

                return;
            }
            throw new Exception("AppKey:" + appKey + " not registered");
        }

        private bool ValidateKey(string appKey) {
            // Build Logic out of configurations
            return true;
        }

        private async Task Execute(string appKey, string email, string subject, string message, List<string> attachmentPath)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email)
                                 ? _emailSettings.ToEmail
                                 : email;
                if (_emailSettings.IsFake) {
                    toEmail = _emailSettings.ToEmail;
                }
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Sumit Bakshi")
                };
                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = appKey + " System - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
