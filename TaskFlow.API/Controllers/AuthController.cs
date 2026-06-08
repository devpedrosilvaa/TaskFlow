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

        public AuthController(
            RegisterUserService registerService,
            LoginService loginService)
        {
            _registerService = registerService;
            _loginService = loginService;
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
                accessToken = token
            });
        }
    }
}
