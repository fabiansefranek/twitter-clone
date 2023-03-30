using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using twitter_clone.Models;

namespace twitter_clone.controllers;

public class FollowController
{
	public static async Task<IResult> Follow([FromBody] User followed, User user, TwitterCloneContext db)
	{
		var followedUser = await db.Users.FindAsync(followed.Id);
		if (followedUser == null) return Utils.Response("User to be followed does not exist", "", HttpStatusCode.NotFound);

		var exists = db.Follows.Any(f => f.FollowedId == followedUser.Id && f.FollowerId == user.Id);
		if (exists) return Utils.Response("", "", HttpStatusCode.NotFound);

		var follow = new Follow()
		{
			Followed = new UserDTO(followedUser), FollowedId = followed.Id, Follower = new UserDTO(user), FollowerId = user.Id,
			CreatedAt = Utils.GetTimestamp()
		};
		db.Follows.Add(follow);
		await db.SaveChangesAsync();
		return Utils.Response("", "", HttpStatusCode.OK);
	}

	public static async Task<IResult> Unfollow([FromBody] User unfollowed, User user, TwitterCloneContext db)
	{
		var unfollowedUser = await db.Users.FindAsync(unfollowed.Id);
		if (unfollowedUser == null)
		{
			return Utils.Response("User to be unfollowed does not exist", "", HttpStatusCode.NotFound);
		}

		var follow = await db.Follows.Where(f => f.FollowedId == unfollowedUser.Id && f.FollowerId == user.Id).FirstOrDefaultAsync();
		if (follow == null) return Utils.Response("", "", HttpStatusCode.NotFound);

		db.Follows.Remove(follow);
		await db.SaveChangesAsync();
		return Utils.Response("", "", HttpStatusCode.OK);
	}


	public static async Task<IResult> IsFollowing(int userId, User user, TwitterCloneContext db)
	{
		var isFollowing = await db.Follows.FirstOrDefaultAsync(f => f.FollowedId == userId && f.FollowerId == user.Id);
		if (isFollowing == null)
		{
			return Utils.Response("", false, HttpStatusCode.OK);
		}
		else
		{
			return Utils.Response("", true, HttpStatusCode.OK);
		}
	}

	public static async Task<IResult> GetFollows(User user, TwitterCloneContext db)
	{
		var follows = db.Follows.Where(f => f.FollowerId == user.Id);
		return Utils.Response("", follows, HttpStatusCode.OK);
	}
}
