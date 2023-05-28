using System.ComponentModel.DataAnnotations;

namespace PracticeAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [MinLength(15)]
        public string FirstName { get; set; } = string.Empty;

        [MinLength(15)]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateOnly Birthday { get; set; }

        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public DateOnly LastAuth { get; set; }

        public int AuthFailedCount { get; set; }
        public bool IsLocked { get; set; }
    }
}
