namespace twitter_clone.Models;

public class PostDTO
{
	public int Id { get; set; }
	public UserDTO User { get; set; }
	public string Text { get; set; }
	public int CreatedAt { get; set; }
	public int UpdatedAt { get; set; }
}
