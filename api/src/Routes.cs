using twitter_clone.controllers;
using twitter_clone.Models;

namespace twitter_clone;

public class Routes
{
    public static void Mount(WebApplication app)
    {
        app.MapPost("/auth/login", AuthenticationController.Login);
        app.MapPost("/auth/signup", AuthenticationController.Signup);
        app.MapPost("/auth/validate", AuthenticationController.ValidateToken);
        app.MapGet("/auth/me", AuthenticationController.GetUserInfo).RequireAuthorization("user");

        app.MapGet("/posts", (TwitterCloneContext db) => PostController.GetPosts(-1, db));
        app.MapGet("/posts/{id:int}", PostController.GetPost);
        app.MapPost("/posts", PostController.CreatePost).RequireAuthorization("user");
        app.MapPut("/posts", PostController.UpdatePost).RequireAuthorization("user");
        app.MapDelete("/posts/{id:int}", PostController.DeletePost).RequireAuthorization("user");

        app.MapGet("/users", UserController.GetUsers);
        app.MapGet("/users/{id:int}", UserController.GetUser);
        app.MapPut("/users", UserController.UpdateUser).RequireAuthorization("user");
        app.MapDelete("/users", UserController.DeleteUser).RequireAuthorization("user");
        app.MapGet("/users/{id:int}/posts", PostController.GetPosts);


        app.MapGet(
		        "/follows",
		        (User user, TwitterCloneContext db) =>
			        FollowController.GetFollows(user.Id, user, db)
	        )
	        .RequireAuthorization("user");
        app.MapGet("/follows/{userId:int}", FollowController.GetFollows)
	        .RequireAuthorization("user");
        app.MapPost("/follows/{userId:int}", FollowController.Follow).RequireAuthorization("user");
        app.MapDelete("/follows/{userId:int}", FollowController.Unfollow)
            .RequireAuthorization("user");


        app.MapGet(
		        "/followers",
		        (User user, TwitterCloneContext db) =>
			        FollowController.GetFollowers(user.Id, user, db)
	        )
	        .RequireAuthorization("user");
        app.MapGet("/followers/{userId:int}", FollowController.GetFollowers)
            .RequireAuthorization("user");

        app.MapGet("/likes", LikeController.GetLikes).RequireAuthorization("user");
        app.MapGet("/likes/{postId:int}", LikeController.IsLiking).RequireAuthorization("user");
        app.MapPost("/likes", LikeController.Like).RequireAuthorization("user");
        app.MapDelete("/likes", LikeController.Unlike).RequireAuthorization("user");

        app.MapGet("/reports", ReportController.GetReports).RequireAuthorization("moderator");
        app.MapGet("/reports/{reportId:int}", ReportController.GetReport)
            .RequireAuthorization("moderator");
        app.MapPost("/reports", ReportController.CreateReport).RequireAuthorization("moderator");
        app.MapPut("/reports", ReportController.UpdateReport).RequireAuthorization("moderator");
        app.MapDelete("/reports", ReportController.DeleteReport).RequireAuthorization("moderator");
    }
}
