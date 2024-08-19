using MimeKit;
using MailKit.Net.Smtp;
using System.Net;
using SocialNetwork.Core.Contracts.MailkitNotification;
using SocialNetwork.MailKitNotificationService;

namespace SocialNetwork.MailKitNotificationSvc.Services
{
    public class MailkitEmailSender : IEmailSender
    {
        private readonly SmtpConfig _smtpOptions;
        private readonly SmtpClient _smtpClient = new();

        public MailkitEmailSender(SmtpConfig smtpOptions)
        {
            _smtpOptions = smtpOptions ?? throw new ArgumentException(nameof(smtpOptions));
        }

        private async Task EnsureConnectAndAuthenticateAsync(CancellationToken cancellationToken)
        {
            if (!_smtpClient.IsConnected)
            {
                await _smtpClient.ConnectAsync
                    (_smtpOptions.Host, _smtpOptions.Port, true, cancellationToken);
            }
            if (!_smtpClient.IsAuthenticated)
            {
                await _smtpClient.AuthenticateAsync
                    (_smtpOptions.UserName, _smtpOptions.Password, cancellationToken);
            }
        }

        public async Task SendEmailAsync(string recepientEmail, string subject,
          string message, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(recepientEmail);
            ArgumentNullException.ThrowIfNull(subject);
            ArgumentNullException.ThrowIfNull(message);

            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("PetSocialNetwork", _smtpOptions.UserName));
            emailMessage.To.Add(new MailboxAddress("", recepientEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            await EnsureConnectAndAuthenticateAsync(cancellationToken);
            await _smtpClient.SendAsync(emailMessage, cancellationToken);
        }
    }
}


