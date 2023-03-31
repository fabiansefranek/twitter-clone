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
        if (requestUser.Username.IsNullOrEmpty() || requestUser.Password.IsNullOrEmpty())
            return Utils.Response(
                "Username and/or password were not provided.",
                "",
                0,
                HttpStatusCode.Conflict
            );
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username.Equals(requestUser.Username));
        if (user is null)
            return Utils.Response(
                "User with this username not found in database.",
                "",
                0,
                HttpStatusCode.NotFound
            );

        var hashedPassword = AuthenticationService.HashString(requestUser.Password);
        if (!hashedPassword.Equals(user.Password))
            return Utils.Response("Password is incorrect.", "", 0, HttpStatusCode.Unauthorized);

        var token = AuthenticationService.GenerateToken(user);

        return Utils.Response("", token, 1, HttpStatusCode.OK);
    }

    public static async Task<IResult> Signup([FromBody] User user, TwitterCloneContext db)
    {
        var userExists =
            await db.Users.FirstOrDefaultAsync(u => u.Username.Equals(user.Username)) != null;
        if (userExists)
            return Utils.Response(
                "User with that username already exists.",
                "",
                0,
                HttpStatusCode.Conflict
            );

        if (
            user.Password.IsNullOrEmpty()
            || user.Username.IsNullOrEmpty()
            || user.Fullname.IsNullOrEmpty()
        )
            return Utils.Response(
                "Not all user details were provided. (username, password, fullname)",
                "",
                0,
                HttpStatusCode.Conflict
            );

        var passwordHash = AuthenticationService.HashString(user.Password);
        user.Password = passwordHash;
        user.Role = "user";
        user.CreatedAt = Utils.GetTimestamp();

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var token = AuthenticationService.GenerateToken(user);
        return Utils.Response("", token, 1, HttpStatusCode.OK);
    }

    public static IResult GetUserInfo(User user)
    {
        return Utils.Response("", user, 1, HttpStatusCode.OK);
    }

    public static IResult ValidateToken([FromBody] string token)
    {
        bool tokenIsValid = AuthenticationService.IsTokenValid(token);
        if (tokenIsValid)
            return Utils.Response("", "", 0, HttpStatusCode.OK);
        else
            return Utils.Response("Token is invalid.", "", 0, HttpStatusCode.Unauthorized);
    }
}
