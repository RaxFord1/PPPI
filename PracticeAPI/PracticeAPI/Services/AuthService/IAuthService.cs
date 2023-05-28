using PracticeAPI.DTO;
using PracticeAPI.DTO.User;
using PracticeAPI.Models;

namespace PracticeAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<BaseResponse<AuthResponse>> Login(LoginUserRequest request);
        Task<BaseResponse<UserResponse>> Register(RegisterUserRequest request);
    }
}
