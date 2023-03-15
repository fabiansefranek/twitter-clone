using System.Net;
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
}
