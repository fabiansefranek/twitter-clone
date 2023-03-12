using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using twitter_clone.Models;
using twitter_clone.services;

namespace twitter_clone;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")!;
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        services.AddCors();
        services.AddDbContext<TwitterCloneContext>();
        services.AddHttpContextAccessor();
        services.AddTransient<User>(provider => // Provides User Injectable to be used in controllers
        {
            ClaimsPrincipal? principal = provider
                .GetService<IHttpContextAccessor>()
                ?.HttpContext?.User;
            if (principal == null)
                return new User { Id = -1 };

            var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = principal.FindFirstValue(ClaimTypes.Name);
            var role = principal.FindFirstValue(ClaimTypes.Role);

            return new User
            {
                Id = Convert.ToInt32(id),
                Username = username!,
                Role = role!
            };
        });
        services.AddSingleton<AuthenticationService>();
        services
            .AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("user", policy => policy.RequireAuthenticatedUser());
            options.AddPolicy("moderator", policy => policy.RequireRole("moderator"));
        });
    }

    public void Configure(WebApplication app, IWebHostEnvironment env, TwitterCloneContext context)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors(
            (
                cors =>
                    cors.AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true)
                        .AllowCredentials()
            )
        );

        // Auto Migrations
        context.Database.EnsureCreated();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}
