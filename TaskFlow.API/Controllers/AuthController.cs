using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs.Auth;
using TaskFlow.Application.Services;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserService _registerService;

        private readonly LoginService _loginService;
        private readonly LogoutService _logoutService;
        private readonly RefreshTokenService _refreshTokenService;

        public AuthController(
            RegisterUserService registerService,
            LoginService loginService,
            LogoutService logoutService,
            RefreshTokenService refreshTokenService)
        {
            _registerService = registerService;
            _loginService = loginService;
            _logoutService = logoutService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterUserRequest request)
        {
            await _registerService.RegisterAsync(
                request);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequest request)
        {
            var token =
                await _loginService.LoginAsync(
                    request);

            return Ok(new
            {
                LoginResponse = token
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(
            RefreshTokenRequest request)
        {
            var token =
                await _refreshTokenService.RefreshTokenAsync(
                    request);

            return Ok(new
            {
                AccessToken = token
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(
            RefreshTokenRequest request)
        {
            await _logoutService.LogoutAsync(
                    request);

            return Ok();
        }
    }
}
