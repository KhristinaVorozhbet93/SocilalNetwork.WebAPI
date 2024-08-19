namespace SocialNetwork.Core.Contracts.MailkitNotification
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string recepientEmail, string subject,
          string message, CancellationToken cancellationToken);
    }
}
