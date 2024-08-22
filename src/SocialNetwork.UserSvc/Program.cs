using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Core.Contracts.Infrastructure;
using SocialNetwork.Core.Contracts.Infrastructure.Options;
using SocialNetwork.UserSvc.Filters;
using SocialNetwork.UserSvc.Repositories;
using SocialNetwork.UserSvc.Services;

namespace SocialNetwork.UserSvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            JwtConfig? jwtConfig = builder.Configuration
               .GetRequiredSection("JwtConfig")
               .Get<JwtConfig>();
            if (jwtConfig is null)
            {
                throw new InvalidOperationException("JwtConfig is not configured");
            }
            builder.Services.AddSingleton(jwtConfig);

            builder.Services.AddCors();
            builder.Services.AddControllers();
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<CentralizedExceptionHandlingFilter>();
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<UserDbContext>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped(typeof(IRepositoryEF<>), typeof(EFRepository<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IApplicationPasswordHasher, IdentityPasswordHasher>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
                     ValidateIssuerSigningKey = true,
                     ValidateLifetime = true,
                     RequireExpirationTime = true,
                     RequireSignedTokens = true,
                     ValidateAudience = true,
                     ValidateIssuer = true,
                     ValidAudiences = new[] { jwtConfig.Audience },
                     ValidIssuer = jwtConfig.Issuer
                 };
             });

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
