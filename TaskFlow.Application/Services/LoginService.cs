using System.Security.Cryptography;
using TaskFlow.Application.DTOs.Auth;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Services
{
    public class LoginService
    {
        private readonly IUserRepository _repository;
        private readonly IRefreshTokenRepository _refreshRepository;
        private readonly ITokenService _tokenService;

        public LoginService(
            IUserRepository repository,
            IRefreshTokenRepository refreshRepository,
            ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _refreshRepository = refreshRepository;
        }

        public async Task<LoginResponse> LoginAsync(
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

            var accessToken =
                _tokenService.Generate(user);

            var refreshToken =
                RefreshTokenGenerator.GenerateRefreshToken();

            var entity =
                new RefreshToken(
                    refreshToken,
                    user.Id,
                    DateTime.UtcNow.AddDays(7));

            await _refreshRepository
                .AddAsync(entity);

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
