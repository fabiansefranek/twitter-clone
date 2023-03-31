using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using twitter_clone.Models;

namespace twitter_clone.controllers;

public class LikeController
{
    public static async Task<IResult> Like([FromBody] Post post, User user, TwitterCloneContext db)
    {
        var existingPost = await db.Posts.FindAsync(post.Id);
        if (existingPost == null)
            return Utils.Response(
                "Post to be liked does not exist",
                "",
                0,
                HttpStatusCode.NotFound
            );

        var likeExists = db.Likes.Any(l => l.UserId == user.Id && l.PostId == post.Id);
        if (likeExists)
            return Utils.Response("Post was already liked", "", 0, HttpStatusCode.Conflict);

        var like = new Like()
        {
            PostId = existingPost.Id,
            UserId = user.Id,
            CreatedAt = Utils.GetTimestamp()
        };
        db.Likes.Add(like);
        await db.SaveChangesAsync();
        return Utils.Response("", like, 1, HttpStatusCode.OK);
    }

    public static async Task<IResult> Unlike(
        [FromBody] Post post,
        User user,
        TwitterCloneContext db
    )
    {
        var existingPost = await db.Posts.FindAsync(post.Id);
        if (existingPost == null)
        {
            return Utils.Response(
                "Post to be unliked does not exist",
                "",
                0,
                HttpStatusCode.NotFound
            );
        }

        var like = await db.Likes
            .Where(l => l.UserId == user.Id && l.PostId == post.Id)
            .FirstOrDefaultAsync();
        if (like == null)
            return Utils.Response(
                "Like to be removed does not exist",
                "",
                0,
                HttpStatusCode.NotFound
            );

        db.Likes.Remove(like);
        await db.SaveChangesAsync();
        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }

    public static async Task<IResult> IsLiking(int postId, User user, TwitterCloneContext db)
    {
        var isLiking = await db.Likes.FirstOrDefaultAsync(
            l => l.PostId == postId && l.UserId == user.Id
        );
        return Utils.Response("", isLiking != null, 1, HttpStatusCode.OK);
    }

    public static async Task<IResult> GetLikes(User user, TwitterCloneContext db)
    {
        var likes = await db.Likes.Where(l => l.UserId == user.Id).ToListAsync();
        return Utils.Response("", likes, likes.Count, HttpStatusCode.OK);
    }
}
