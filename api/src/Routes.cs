using System.Security.Claims;
using twitter_clone.controllers;
using twitter_clone.Models;

namespace twitter_clone;

public class Routes
{
    public void Mount(WebApplication app)
    {
        app.MapPost("/auth/login", AuthenticationController.Login);
        app.MapPost("/auth/signup", AuthenticationController.Signup);
        app.MapPost("/auth/validate", AuthenticationController.ValidateToken);
        app.MapGet("/auth/me", AuthenticationController.GetUserInfo).RequireAuthorization("user");

        app.MapGet("/posts", PostController.GetPosts);
        app.MapGet("/posts/{id}", PostController.GetPost);
        app.MapPost("/posts", PostController.AddPost).RequireAuthorization("user");
        app.MapPut("/posts", PostController.UpdatePost).RequireAuthorization("user");
        app.MapDelete("/posts", PostController.DeletePost).RequireAuthorization("user");

        app.MapGet("/users", (TwitterCloneContext db) => db.Users.ToList());
        app.MapGet("/users/{username}", UserController.GetUser);

        app.MapPost("/follow", FollowController.Follow).RequireAuthorization("user");
        app.MapPost("/unfollow", FollowController.Unfollow).RequireAuthorization("user");
        app.MapGet("/follows/{userId}", FollowController.IsFollowing).RequireAuthorization("user");
        app.MapGet("/follows", FollowController.GetFollows).RequireAuthorization("user");

    }
}
