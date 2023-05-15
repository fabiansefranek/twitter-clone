using System.ComponentModel.DataAnnotations.Schema;

namespace twitter_clone.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Biography { get; set; }
        public string ProfilePicture { get; set; }
        public string Role { get; set; }
        public int CreatedAt { get; set; }
        public List<Post> Posts { get; set; }
        public List<Like> Likes { get; set; }
        public List<Follow> Follows { get; set; }
    }
}
