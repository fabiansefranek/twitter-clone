using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using twitter_clone.Models;

namespace twitter_clone.controllers;

public class UserController
{
    public static async Task<IResult> GetUsers(TwitterCloneContext db)
    {
        var users = await db.Users.Select(u => new UserDTO(u)).ToListAsync();
        return Utils.Response("", users, users.Count, HttpStatusCode.OK);
    }

    public static async Task<IResult> GetUser(string username, TwitterCloneContext db)
    {
	    var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
        {
            return Utils.Response(
                "User with this username not found",
                "",
                0,
                HttpStatusCode.NotFound
            );
        }

        return Utils.Response("", new UserDTO(user), 1, HttpStatusCode.OK);
    }

    public static async Task<IResult> DeleteUser(
        [FromBody] User deletedUser,
        User user,
        TwitterCloneContext db
    )
    {
        var existingUser = await db.Users.FindAsync(
            user.Role == "moderator" ? deletedUser.Id : user.Id
        );
        if (existingUser == null)
        {
            return Utils.Response("User to be deleted not found", "", 0, HttpStatusCode.NotFound);
        }

        if (user.Role != "moderator" && existingUser.Id != user.Id)
        {
            return Utils.Response(
                "User is not authorized to delete this user",
                "",
                0,
                HttpStatusCode.Forbidden
            );
        }

        db.Users.Remove(existingUser);
        await db.SaveChangesAsync();

        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }

    public static async Task<IResult> UpdateUser(
        [FromBody] User updatedUser,
        User user,
        TwitterCloneContext db
    )
    {
        var existingUser = await db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == updatedUser.Id);
        if (existingUser == null)
        {
            return Utils.Response("Post not found", "", 0, HttpStatusCode.NotFound);
        }

        if (existingUser.Id != user.Id)
        {
            return Utils.Response(
                "User is not authorized to update this user",
                "",
                0,
                HttpStatusCode.Forbidden
            );
        }

        existingUser.Username = updatedUser.Username.IsNullOrEmpty()
            ? existingUser.Username
            : updatedUser.Username;
        existingUser.Fullname = updatedUser.Fullname.IsNullOrEmpty()
            ? existingUser.Fullname
            : updatedUser.Fullname;
        existingUser.Role = updatedUser.Role.IsNullOrEmpty() ? existingUser.Role : updatedUser.Role;
        // TODO: re-issue token after updating
        db.Users.Update(existingUser);
        await db.SaveChangesAsync();

        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }
}
