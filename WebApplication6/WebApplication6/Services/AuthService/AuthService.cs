using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PracticeAPI.DTO.User;
using PracticeAPI.DTO;
using PracticeAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PracticeAPI.Services.PasswordService;
using PracticeAPI.Extensions;

namespace PracticeAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;
        private List<User> _users;

        public AuthService(IConfiguration configuration, IPasswordService passwordService)
        {
            _configuration = configuration;
            _passwordService = passwordService;

            _users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "alice.smith@example.com",
                    Birthday = new DateOnly(1985, 3, 15),
                    LastAuth = DateOnly.FromDateTime(DateTime.Now),
                    AuthFailedCount = 1,
                    IsLocked = false
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Bob",
                    LastName = "Johnson",
                    Email = "bob.johnson@example.com",
                    Birthday = new DateOnly(1991, 6, 20),
                    LastAuth = DateOnly.FromDateTime(DateTime.Now),
                    AuthFailedCount = 0,
                    IsLocked = false
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Charlie",
                    LastName = "Williams",
                    Email = "charlie.williams@example.com",
                    Birthday = new DateOnly(1997, 12, 5),
                    LastAuth = DateOnly.FromDateTime(DateTime.Now),
                    AuthFailedCount = 2,
                    IsLocked = false
                }
            };
            _passwordService.SetUserPasswordHash(_users[0], "12345");
            _passwordService.SetUserPasswordHash(_users[1], "pass");
            _passwordService.SetUserPasswordHash(_users[2], "test");
        }

        public async Task<BaseResponse<AuthResponse>> Login(LoginUserRequest request)
        {
            try
            {
                var failResponse = new BaseResponse<AuthResponse>
                {
                    Message = "Authentication failed",
                    ValueCount = 1,
                    Values = new List<AuthResponse> { new AuthResponse() { Message = "Authentication failed" } }
                };

                if (_users.Count == 0)
                {
                    return failResponse;
                }
                var user = await Task.FromResult(_users.Find(x => x.Email.Equals(request.Email)));
                if (user == null)
                {
                    return failResponse;
                }

                if (!_passwordService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    user.AuthFailedCount++;
                    bool isLockThresholdParsed = int.TryParse(
                            _configuration.GetSection("Authorization:LockThreshold").Value,
                            out var lockThreshold
                        );
                    if (isLockThresholdParsed && user.AuthFailedCount >= lockThreshold) { user.IsLocked = true; }
                    return failResponse;
                }

                user.LastAuth = DateOnly.FromDateTime(DateTime.Now);
                string token = CreateToken(user);

                var response = new AuthResponse
                {
                    Success = true,
                    UserName = $"{user.FirstName} {user.LastName}",
                    Token = token,
                };
                return new BaseResponse<AuthResponse>
                {
                    Success = response.Success,
                    Message = "Success in auth",
                    StatusCode = StatusCodes.Status200OK,
                    ValueCount = 1,
                    Values = new List<AuthResponse> { response }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AuthResponse>()
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<BaseResponse<UserResponse>> Register(RegisterUserRequest request)
        {
            try
            {
                if (await Task.Run(() => _users.Find(x => request.Email.Equals(x.Email))) != null)
                {
                    return new BaseResponse<UserResponse>()
                    {
                        Message = "The email address is already in use",
                        StatusCode = StatusCodes.Status409Conflict,
                    };
                }

                var user = request.ToModel();
                _passwordService.SetUserPasswordHash(user, request.Password);
                user.Id = Guid.NewGuid();

                _users.Add(user);
                return new BaseResponse<UserResponse>()
                {
                    Message = "User registered",
                    StatusCode = StatusCodes.Status201Created,
                    ValueCount = 1,
                    Values = new List<UserResponse> { user.ToResponse() }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserResponse>()
                {
                    Message = ex.Message
                };
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, value: $"{user.FirstName} {user.LastName}")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Authorization:TokenKey").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
