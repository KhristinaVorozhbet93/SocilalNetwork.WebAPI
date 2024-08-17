using Microsoft.EntityFrameworkCore;
using SocialNetwork.Shared;
using SocialNetwork.Shared.Interfaces;

namespace SocialNetwork.WebAPI.AccountService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var msSqlConfig = builder.Configuration
            .GetRequiredSection("MsSqlConfig")
            .Get<MsSqlConfig>();
            if (msSqlConfig is null)
            {
                throw new InvalidOperationException("MsSqlConfig is not configured");
            }

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AccountDbContext>(options =>
            options.UseNpgsql
            ($"Server = {msSqlConfig.ServerName}; Database = SocialNetwork; " +
            $"User Id = {msSqlConfig.UserName}; Password = {msSqlConfig.Password}; TrustServerCertificate=True"));

            builder.Services.AddScoped(typeof(IRepositoryEF<>), typeof(EFRepository<>));
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<IApplicationPasswordHasher, IdentityPasswordHasher>();

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
