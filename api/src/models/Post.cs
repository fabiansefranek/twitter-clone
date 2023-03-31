using System.ComponentModel.DataAnnotations.Schema;

namespace twitter_clone.Models;

public class Post
{
    public Post() { }

    public Post(User user, string text)
    {
        User = user;
        UserId = user.Id;
        Text = text;
        CreatedAt = Utils.GetTimestamp();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; }
    public int CreatedAt { get; set; }
    public int UpdatedAt { get; set; }
    public List<Like> Likes { get; set; }
}
