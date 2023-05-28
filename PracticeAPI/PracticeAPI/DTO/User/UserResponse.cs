using System.ComponentModel.DataAnnotations;

namespace PracticeAPI.DTO.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public DateOnly LastAuth { get; set; }
    }
}
