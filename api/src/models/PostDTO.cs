namespace twitter_clone.Models;

public class PostDTO
{
	public PostDTO() {}
	public PostDTO(Post post)
	{
		Id = post.Id;
		User = new UserDTO(post.User);
		Text = post.Text;
		CreatedAt = post.CreatedAt;
		UpdatedAt = post.UpdatedAt;
	}
	public int Id { get; set; }
	public UserDTO User { get; set; }
	public string Text { get; set; }
	public int CreatedAt { get; set; }
	public int UpdatedAt { get; set; }
}
