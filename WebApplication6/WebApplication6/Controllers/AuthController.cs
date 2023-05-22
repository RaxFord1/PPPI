using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.DTO;
using PracticeAPI.DTO.User;
using PracticeAPI.Services.AuthService;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseResponse<UserResponse>>> Register(RegisterUserRequest request)
        {
            var response = await _authService.Register(request);
            return response.StatusCode switch
            {
                StatusCodes.Status201Created => Ok(response),
                StatusCodes.Status409Conflict => Conflict(response),
                _ => BadRequest(response),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<AuthResponse>>> Login(LoginUserRequest request)
        {
            var response = await _authService.Login(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
