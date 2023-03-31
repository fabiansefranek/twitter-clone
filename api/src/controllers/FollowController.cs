using System.Net;
using Microsoft.EntityFrameworkCore;
using twitter_clone.Models;

namespace twitter_clone.controllers;

public class FollowController
{
    public static async Task<IResult> Follow(int userId, User user, TwitterCloneContext db)
    {
        var followedUser = await db.Users.FindAsync(userId);
        if (followedUser == null)
            return Utils.Response(
                "User to be followed does not exist",
                "",
                0,
                HttpStatusCode.NotFound
            );

        var currentUser = await db.Users.FindAsync(user.Id);
        if (currentUser == null)
        {
            return Utils.Response(
                "Authenticated user does not exist",
                "",
                0,
                HttpStatusCode.NotFound
            );
        }

        var followExists = db.Follows.Any(
            f => f.FollowedId == followedUser.Id && f.FollowerId == currentUser.Id
        );
        if (followExists)
            return Utils.Response(
                "Person to be followed was already followed",
                "",
                0,
                HttpStatusCode.Conflict
            );

        db.Follows.Add(new Follow(followed: followedUser, follower: currentUser));
        await db.SaveChangesAsync();

        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }

    public static async Task<IResult> Unfollow(int userId, User user, TwitterCloneContext db)
    {
        var unfollowedUser = await db.Users.FindAsync(userId);
        if (unfollowedUser == null)
            return Utils.Response(
                "User to be unfollowed does not exist",
                "",
                0,
                HttpStatusCode.NotFound
            );

        var currentUser = await db.Users.FindAsync(user.Id);
        if (currentUser == null)
        {
            return Utils.Response(
                "Authenticated user does not exist",
                "",
                0,
                HttpStatusCode.NotFound
            );
        }

        var existingFollow = await db.Follows.FirstOrDefaultAsync(
            f => f.FollowedId == unfollowedUser.Id && f.FollowerId == currentUser.Id
        );
        if (existingFollow == null)
            return Utils.Response(
                "Person to be unfollowed wasn't followed before",
                "",
                0,
                HttpStatusCode.Conflict
            );

        db.Follows.Remove(existingFollow);
        await db.SaveChangesAsync();

        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }

    public static async Task<IResult> GetFollows(int userId, User user, TwitterCloneContext db)
    {
        var follows = await db.Follows.Where(f => f.FollowerId == userId).ToListAsync();
        return Utils.Response("", follows, follows.Count, HttpStatusCode.OK);
    }

    public static async Task<IResult> GetFollowers(int userId, User user, TwitterCloneContext db)
    {
        var followers = await db.Follows.Where(f => f.FollowedId == userId).ToListAsync();
        return Utils.Response("", followers, followers.Count, HttpStatusCode.OK);
    }
}
