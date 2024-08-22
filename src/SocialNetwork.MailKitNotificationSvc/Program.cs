using SocialNetwork.Core.Contracts.Infrastructure.Options;
using SocialNetwork.Core.Contracts.MailkitNotification;
using SocialNetwork.MailKitNotificationSvc.Services;

namespace SocialNetwork.MailKitNotificationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SmtpConfig? smtpConfig = builder.Configuration
                .GetRequiredSection("SmtpConfig")
                .Get<SmtpConfig>();
            if (smtpConfig is null)
            {
                throw new InvalidOperationException("SmtpConfig is not configured");
            }
            builder.Services.AddSingleton(smtpConfig);

            builder.Services.AddScoped<IEmailSender, MailkitEmailSender>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
