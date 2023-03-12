using twitter_clone.Models;

namespace twitter_clone.controllers;

public class PostController
{
	public static async Task<dynamic> AddPost(Post post, int userId, TwitterCloneContext db)
	{
		var user = await db.Users.FindAsync(userId);
		if (user is null)
		{
			return Results.NotFound();
		}

		db.Posts.Add(post);
		await db.SaveChangesAsync();
		return Results.Ok();
	}
}
