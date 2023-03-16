using System.Net;
using Microsoft.EntityFrameworkCore;
using twitter_clone.Models;

namespace twitter_clone.controllers;

public class PostController
{
    public static async Task<IResult> AddPost(Post post, User user, TwitterCloneContext db)
    {
	    if (user.Id == -1) return Utils.Response("Token could not be parsed or no user information was found in the token provided.", "", HttpStatusCode.Conflict);

        var userExists = await db.Users.FindAsync(user.Id) != null;
        if (!userExists)
            return Utils.Response($"User with ID {user.Id} was not found in the database.", "", HttpStatusCode.NotFound);

        db.Posts.Add(
            new Post
            {
                UserId = user.Id,
                CreatedAt = Utils.GetTimestamp(),
                Text = post.Text
            }
        );
        await db.SaveChangesAsync();

        return Utils.Response("", "", HttpStatusCode.OK);
    }

    public static IResult GetPosts(TwitterCloneContext db)
    {
	    var posts = db.Posts.OrderByDescending(p => p.CreatedAt).Include(p => p.User);
	    var postsDto = posts.Select(p => new PostDTO() {Id = p.Id, User = new UserDTO() {Id=p.User.Id,Username = p.User.Username,Fullname = p.User.Fullname, Role = p.User.Role, CreatedAt = p.User.CreatedAt}, Text = p.Text, CreatedAt = p.CreatedAt, UpdatedAt = p.UpdatedAt});
	    return Utils.Response("", postsDto, HttpStatusCode.OK);
    }
}
