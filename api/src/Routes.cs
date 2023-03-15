using System.Security.Claims;
using twitter_clone.controllers;

namespace twitter_clone;

public class Routes
{
    public void Mount(WebApplication app)
    {
        app.MapGet("/users", (TwitterCloneContext db) => db.Users.ToList());

        app.MapPost("/auth/login", AuthenticationController.Login);
        app.MapPost("/auth/signup", AuthenticationController.Signup);
        app.MapPost("/auth/validate", AuthenticationController.ValidateToken);
        app.MapGet("/auth/me", AuthenticationController.GetUserInfo).RequireAuthorization("user");

        app.MapGet("/posts", PostController.GetPosts);
        app.MapPost("/posts", PostController.AddPost).RequireAuthorization("user");
    }
}
