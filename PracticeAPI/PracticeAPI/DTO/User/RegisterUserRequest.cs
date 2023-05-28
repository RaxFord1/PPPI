using System.ComponentModel.DataAnnotations;

namespace PracticeAPI.DTO.User
{
    public class RegisterUserRequest
    {
        [Required, MaxLength(15)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateOnly Birthday { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
