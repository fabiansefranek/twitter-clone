using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using twitter_clone.Models;

namespace twitter_clone.controllers;

public class UserController
{
	public static async Task<IResult> GetUser(string username, TwitterCloneContext db)
	{
		var user = await db.Users.FirstOrDefaultAsync(user => user.Username == username);
		if (user == null)
		{
			return Utils.Response("User with this username not found", "", HttpStatusCode.NotFound);
		}

		return Utils.Response("",
			new UserDTO(user),
		HttpStatusCode.OK
		);


	}
}
