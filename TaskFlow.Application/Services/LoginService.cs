using TaskFlow.Application.DTOs.Auth;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Services
{
    public class LoginService
    {
        private readonly IUserRepository _repository;

        private readonly ITokenService _tokenService;

        public LoginService(
            IUserRepository repository,
            ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(
            LoginRequest request)
        {
            var user =
                await _repository.GetUserByEmailAsync(
                    request.Email);

            if (user is null)
                throw new Exception(
                    "Credenciais inválidas");

            var validPassword =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!validPassword)
                throw new Exception(
                    "Credenciais inválidas");

            return _tokenService.Generate(user);
        }
    }
}
