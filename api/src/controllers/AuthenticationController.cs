using Microsoft.EntityFrameworkCore;
using twitter_clone.Models;
using twitter_clone.services;

namespace twitter_clone.controllers;

public class AuthenticationController
{
    public static async Task<dynamic> Login(User requestUser, TwitterCloneContext db)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username.Equals(requestUser.Username));
        if (user is null)
        {
            return Results.NotFound();
        }

        var hashedPassword = AuthenticationService.HashString(requestUser.Password);
        if (!hashedPassword.Equals(user.Password))
        {
            return Results.Unauthorized();
        }

        var token = AuthenticationService.GenerateToken(user);

        return Results.Ok(new { token = token });
    }

    public static async Task<dynamic> Signup(User user, TwitterCloneContext db)
    {
        var userExists =
            await db.Users.FirstOrDefaultAsync(u => u.Username.Equals(user.Username)) != null;
        if (userExists)
            return Results.Conflict();

        var passwordHash = AuthenticationService.HashString(user.Password);
        user.Password = passwordHash;
        user.Role = "user";
        user.CreatedAt = Convert.ToInt32(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var token = AuthenticationService.GenerateToken(user);

        return Results.Ok(new { token = token });
    }
}
