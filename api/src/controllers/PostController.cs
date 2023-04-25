using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using twitter_clone.Models;

namespace twitter_clone.controllers;

public class PostController
{
    public static async Task<IResult> CreatePost(PostDTO post, User user, TwitterCloneContext db)
    {
        db.Posts.Add(new Post(user: user, text: post.Text));
        await db.SaveChangesAsync();

        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }

    public static async Task<IResult> GetPosts(int id, TwitterCloneContext db)
    {
        List<Post> posts;
        if (id == -1)
        {
            posts = await db.Posts
                .OrderByDescending(p => p.CreatedAt)
                .Include(p => p.User)
                .ToListAsync();
        }
        else
        {
            posts = await db.Posts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.CreatedAt)
                .Include(p => p.User)
                .ToListAsync();
        }

        var postsDto = posts.Select(post => new PostDTO(post));
        return Utils.Response("", postsDto, postsDto.Count(), HttpStatusCode.OK);
    }

    public static async Task<IResult> GetPost(int id, TwitterCloneContext db)
    {
        var post = await db.Posts
            .Include(post => post.User)
            .FirstOrDefaultAsync(post => post.Id == id);
        if (post == null)
        {
            return Utils.Response(
                "Post with this id was not found",
                "",
                0,
                HttpStatusCode.NotFound
            );
        }

        return Utils.Response("", new PostDTO(post), 1, HttpStatusCode.OK);
    }

    public static async Task<IResult> DeletePost(int id, User user, TwitterCloneContext db)
    {
        var post = await db.Posts.FindAsync(id);
        if (post == null)
        {
            return Utils.Response("Post not found", "", 0, HttpStatusCode.NotFound);
        }

        if (user.Role != "moderator" && post.UserId != user.Id)
        {
            return Utils.Response(
                "Post not from authenticated user",
                "",
                0,
                HttpStatusCode.Forbidden
            );
        }

        db.Posts.Remove(post);
        await db.SaveChangesAsync();

        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }

    public static async Task<IResult> UpdatePost(
        [FromBody] Post updatedPost,
        User user,
        TwitterCloneContext db
    )
    {
        var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == updatedPost.Id);
        if (post == null)
        {
            return Utils.Response("Post not found", "", 0, HttpStatusCode.NotFound);
        }

        if (post.UserId != user.Id)
        {
            return Utils.Response(
                "Post not from authenticated user",
                "",
                0,
                HttpStatusCode.Forbidden
            );
        }

        post.Text = updatedPost.Text;

        db.Posts.Update(post);
        await db.SaveChangesAsync();

        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }
}
