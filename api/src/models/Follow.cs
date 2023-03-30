using System.ComponentModel.DataAnnotations.Schema;

namespace twitter_clone.Models;

public class Follow
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public UserDTO Followed { get; set; }
	public int FollowedId { get; set; }
	public UserDTO Follower { get; set; }
	public int FollowerId { get; set; }
	public int CreatedAt { get; set; }
}
