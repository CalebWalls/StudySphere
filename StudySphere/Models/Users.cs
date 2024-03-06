namespace StudySphere.Models
{
    public class Users
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string? ResetToken { get; set; }
    }
}
