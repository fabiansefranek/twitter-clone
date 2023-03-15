using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using twitter_clone.Models;
using twitter_clone.services;

namespace twitter_clone.controllers;

public class AuthenticationController
{
    public static async Task<IResult> Login([FromBody] User requestUser, TwitterCloneContext db)
    {
	    if (requestUser.Username.IsNullOrEmpty() || requestUser.Password.IsNullOrEmpty()) return Utils.Response("Username and/or password were not provided.", "", HttpStatusCode.Conflict);
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username.Equals(requestUser.Username));
        if (user is null)
	        return Utils.Response("User with this username not found in database.", "", HttpStatusCode.NotFound);

        var hashedPassword = AuthenticationService.HashString(requestUser.Password);
        if (!hashedPassword.Equals(user.Password))
	        return Utils.Response("Password is incorrect.", "", HttpStatusCode.Unauthorized);

        var token = AuthenticationService.GenerateToken(user);

        return Utils.Response("", new { token = token }, HttpStatusCode.OK);
    }

    public static async Task<IResult> Signup([FromBody] User user, TwitterCloneContext db)
    {
        var userExists =
            await db.Users.FirstOrDefaultAsync(u => u.Username.Equals(user.Username)) != null;
        if (userExists)
	        return Utils.Response("User with that username already exists.", "", HttpStatusCode.Conflict);

        var passwordHash = AuthenticationService.HashString(user.Password);
        user.Password = passwordHash;
        user.Role = "user";
        user.CreatedAt = Utils.GetTimestamp();

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var token = AuthenticationService.GenerateToken(user);
        return Utils.Response("", new {token=token}, HttpStatusCode.OK);
    }

    public static IResult GetUserInfo(User user)
    {
	    return Utils.Response("", new { id = user.Id, username = user.Username, role = user.Role }, HttpStatusCode.OK);
    }

    public static IResult ValidateToken([FromBody] string token)
    {
	    bool tokenIsValid = AuthenticationService.IsTokenValid(token);
	    if (tokenIsValid) return Utils.Response("", "", HttpStatusCode.OK);
	    else return Utils.Response("Token is invalid.", "", HttpStatusCode.Unauthorized);
    }
}
