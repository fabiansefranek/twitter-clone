using System.Net;

namespace twitter_clone;

public class Utils
{
    public static int GetTimestamp() => Convert.ToInt32(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

    public static IResult Response(dynamic message, dynamic data, int count, HttpStatusCode status)
    {
        return Results.Json(
            new
            {
                message,
                data,
                count
            },
            null,
            null,
            (int)status
        );
    }
}
