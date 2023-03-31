using System.ComponentModel.DataAnnotations.Schema;

namespace twitter_clone.Models;

public class Follow
{
    public Follow() { }

    public Follow(User follower, User followed)
    {
        Follower = follower;
        FollowerId = follower.Id;
        Followed = followed;
        FollowedId = followed.Id;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public User Follower { get; set; }
    public int FollowerId { get; set; }
    public User Followed { get; set; }
    public int FollowedId { get; set; }
    public int CreatedAt { get; set; }
}
