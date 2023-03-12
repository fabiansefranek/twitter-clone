namespace twitter_clone;

public class Utils
{
    public static int GetTimestamp() => Convert.ToInt32(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
}
