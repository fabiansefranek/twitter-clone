namespace twitter_clone.Models
{
    public class UserDTO
    {
        public UserDTO() { }

        public UserDTO(User user)
        {
            Id = user.Id;
            Fullname = user.Fullname;
            Username = user.Username;
            Role = user.Role;
            CreatedAt = user.CreatedAt;
            Biography = user.Biography;
            ProfilePicture = user.ProfilePicture;
        }

        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Biography { get; set; }
        public string ProfilePicture { get; set; }
        public string Role { get; set; }
        public int CreatedAt { get; set; }
    }
}
