using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(
            RefreshToken refreshToken);

        Task<RefreshToken?>
            GetByTokenAsync(string token);

        Task SaveChangesAsync();
        Task UpdateAsync(RefreshToken token);
    }
}
