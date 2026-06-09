using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories
{
    public class RefreshTokenRepository
    : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            RefreshToken refreshToken)
        {
            await _context.RefreshTokens
                .AddAsync(refreshToken);
            await SaveChangesAsync();
        }

        public async Task<RefreshToken?>
            GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.Token == token);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshToken token)
        {
            _context.RefreshTokens
                .Update(token);
            await SaveChangesAsync();
        }
    }
}
