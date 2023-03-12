using twitter_clone.Models;

namespace twitter_clone.controllers;

public class PostController
{
    public static async Task<IResult> AddPost(Post post, User user, TwitterCloneContext db)
    {
        if (user.Id == -1)
            return Results.NotFound("HTTPContext User not found");

        var userExists = await db.Users.FindAsync(user.Id) != null;
        if (!userExists)
            return Results.NotFound("User not found in database");

        db.Posts.Add(
            new Post
            {
                UserId = user.Id,
                CreatedAt = Utils.GetTimestamp(),
                Text = post.Text
            }
        );
        await db.SaveChangesAsync();

        return Results.Ok();
    }
}
