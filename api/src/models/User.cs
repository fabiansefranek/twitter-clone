using System.ComponentModel.DataAnnotations.Schema;

namespace twitter_clone.Models
{
	public class User {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool IsModerator { get; set; }
		public int CreatedAt { get; set; }
	}
}
