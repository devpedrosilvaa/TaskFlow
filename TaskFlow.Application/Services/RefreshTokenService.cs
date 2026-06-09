using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs.Auth;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Services
{
    public class RefreshTokenService
    {

        private readonly IRefreshTokenRepository _refreshRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenService(
            IRefreshTokenRepository refreshRepository,
            ITokenService tokenService)
        {
            _tokenService = tokenService;
            _refreshRepository = refreshRepository;
        }

        public async Task<string> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var token =
                await _refreshRepository
                    .GetByTokenAsync(request.RefreshToken);

            if (token is null)
                throw new Exception("Refresh not found!");

            if (token.Revoked)
                throw new Exception("Refresh revoked");

            if (token.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Refresh expired");

            var accessToken =
                _tokenService.Generate(
                    token.User);

            return accessToken;
        }
    }
}
