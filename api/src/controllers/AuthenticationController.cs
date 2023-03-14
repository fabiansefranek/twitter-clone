using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using twitter_clone.Models;
using twitter_clone.services;

namespace twitter_clone.controllers;

public class AuthenticationController
{
    public static async Task<IResult> Login([FromBody] User requestUser, TwitterCloneContext db)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username.Equals(requestUser.Username));
        if (user is null)
            return Results.NotFound();

        var hashedPassword = AuthenticationService.HashString(requestUser.Password);
        if (!hashedPassword.Equals(user.Password))
            return Results.Unauthorized();

        var token = AuthenticationService.GenerateToken(user);

        return Results.Ok(new { token = token });
    }

    public static async Task<IResult> Signup([FromBody] User user, TwitterCloneContext db)
    {
        var userExists =
            await db.Users.FirstOrDefaultAsync(u => u.Username.Equals(user.Username)) != null;
        if (userExists)
            return Results.Conflict();

        var passwordHash = AuthenticationService.HashString(user.Password);
        user.Password = passwordHash;
        user.Role = "user";
        user.CreatedAt = Utils.GetTimestamp();

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var token = AuthenticationService.GenerateToken(user);

        return Results.Ok(new { token = token });
    }

    public static IResult GetUserInfo(User user)
    {
        return Results.Ok(new {id=user.Id, username=user.Username, role=user.Role});
    }

    public static IResult ValidateToken([FromBody] string token)
    {
	    bool tokenIsValid = AuthenticationService.IsTokenValid(token);
	    if (tokenIsValid)
	    {
		    return Results.Ok(new { message = "Token is valid" });
	    }
	    else
	    {
		    return Results.Unauthorized();
	    }
    }
}
