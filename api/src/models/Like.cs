using System.ComponentModel.DataAnnotations.Schema;

namespace twitter_clone.Models;

public class Like
{
    public Like() { }

    public Like(Post post, User user)
    {
        Post = post;
        PostId = post.Id;
        User = user;
        UserId = user.Id;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Post Post { get; set; }
    public int PostId { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public int CreatedAt { get; set; }
}
