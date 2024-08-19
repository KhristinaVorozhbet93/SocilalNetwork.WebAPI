using Microsoft.EntityFrameworkCore;
using SocialNetwork.Shared;
using SocialNetwork.Shared.Contracts;

namespace SocialNetwork.WebAPI.AccountService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<UserDbContext>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))); 

            builder.Services.AddScoped(typeof(IRepositoryEF<>), typeof(EFRepository<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IApplicationPasswordHasher, IdentityPasswordHasher>();

            var app = builder.Build();

            app.UseCors(policy =>
            {
                policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            });
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
