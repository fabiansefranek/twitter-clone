using System.ComponentModel.DataAnnotations.Schema;

namespace twitter_clone.Models;

public class Report
{
    public Report() { }

    public Report(Post post, string description, User user)
    {
        Post = post;
        PostId = post.Id;
        Description = description;
        UserId = user.Id;
        CreatedAt = Utils.GetTimestamp();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Post Post { get; set; }
    public int PostId { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public int CreatedAt { get; set; }
}
