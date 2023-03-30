using System.ComponentModel.DataAnnotations.Schema;

namespace twitter_clone.Models;

public class Report
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public PostDTO Post { get; set; }
	public string Description { get; set; }
	public UserDTO User { get; set; }
	public int UserId { get; set; }
	public int CreatedAt { get; set; }
}
