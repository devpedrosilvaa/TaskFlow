using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs.Auth;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Services
{
    public class LogoutService
    {
        private readonly IRefreshTokenRepository _refreshRepository;

        public LogoutService(
            IRefreshTokenRepository refreshRepository)
        {
            _refreshRepository = refreshRepository;
        }
        public async Task LogoutAsync(RefreshTokenRequest request)
        {
            var token =
                await _refreshRepository
                    .GetByTokenAsync(request.RefreshToken);
            if(token is null)
                return;

            token.Revoke();

            await _refreshRepository
                .UpdateAsync(token);
        }
    }
}
