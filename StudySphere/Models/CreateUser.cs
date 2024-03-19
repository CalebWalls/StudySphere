using System.ComponentModel.DataAnnotations;

namespace StudySphere.Models
{
    public class CreateUser
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string ConfirmPassword { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
    }
}
