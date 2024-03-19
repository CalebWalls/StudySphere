namespace StudySphere.Models
{
    public class NewPasswordRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set;}
    }
}
