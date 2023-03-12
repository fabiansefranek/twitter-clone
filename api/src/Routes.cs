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

        app.MapGet("/user", (ClaimsPrincipal user) => Results.Ok()).RequireAuthorization("user");
        app.MapGet("/moderator", (ClaimsPrincipal user) => Results.Ok())
            .RequireAuthorization("moderator");

        app.MapGet("/posts", (TwitterCloneContext db) => db.Posts.ToList());
        app.MapPost("/posts", PostController.AddPost).RequireAuthorization("user");
    }
}
