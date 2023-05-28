using PracticeAPI.Models;

namespace PracticeAPI.Services.PasswordService
{
    public interface IPasswordService
    {
        void SetUserPasswordHash(User user, string password);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
