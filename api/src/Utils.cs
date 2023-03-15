using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace twitter_clone;

public class Utils
{
    public static int GetTimestamp() => Convert.ToInt32(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

    public static IResult Response(dynamic message, dynamic data, HttpStatusCode status)
    {
	    return Results.Json(new {message = message, data=data},null,null, (int)status);
    }
}
