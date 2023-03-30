using System.Net;
using Microsoft.AspNetCore.Mvc;
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
	    var postsDto = posts.Select(post => new PostDTO(post));
	    return Utils.Response("", postsDto, HttpStatusCode.OK);
    }

    public static async Task<IResult> GetPost(int id, TwitterCloneContext db)
    {
	    var post = await db.Posts.Include(post => post.User).FirstOrDefaultAsync(post => post.Id == id);
	    if (post == null)
	    {
		    return Utils.Response("Post with this id was not found", "", HttpStatusCode.NotFound);
	    }

	    return Utils.Response("",
		    new PostDTO(post),
		    HttpStatusCode.OK
	    );
    }

    public static async Task<IResult> DeletePost([FromBody] Post requestedPost, User user, TwitterCloneContext db)
    {
	    var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == requestedPost.Id);
	    if (post == null)
	    {
		    return Utils.Response("Post not found", "", HttpStatusCode.NotFound);
	    }

	    if (post.UserId != user.Id)
	    {
		    return Utils.Response("Post not from authenticated user", "", HttpStatusCode.Forbidden);
	    }

	    db.Posts.Remove(post);
	    await db.SaveChangesAsync();

	    return Utils.Response("", "", HttpStatusCode.OK);
    }

    public static async Task<IResult> UpdatePost([FromBody] Post requestedPost, User user, TwitterCloneContext db)
    {
	    var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == requestedPost.Id);
	    if (post == null)
	    {
		    return Utils.Response("Post not found", "", HttpStatusCode.NotFound);
	    }

	    if (post.UserId != user.Id)
	    {
		    return Utils.Response("Post not from authenticated user", "", HttpStatusCode.Forbidden);
	    }

	    db.Posts.Update(post);
	    await db.SaveChangesAsync();

	    return Utils.Response("", "", HttpStatusCode.OK);
    }
}
