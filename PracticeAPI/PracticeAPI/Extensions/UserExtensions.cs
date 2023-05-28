using PracticeAPI.DTO.User;
using PracticeAPI.Models;

namespace PracticeAPI.Extensions
{
    public static class UserExtensions
    {
        public static User ToModel(this RegisterUserRequest request)
        {
            return new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Birthday = request.Birthday,
                Email = request.Email
            };
        }

        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Email = user.Email,
                LastAuth = user.LastAuth
            };
        }
    }
}
