using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using twitter_clone.services;

namespace twitter_clone;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")!;
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        services.AddDbContext<TwitterCloneContext>();
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

        // Auto Migrations
        context.Database.EnsureCreated();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}
