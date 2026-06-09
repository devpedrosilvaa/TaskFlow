using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }

        public string Token { get; private set; }

        public Guid UserId { get; private set; }

        public User User { get; private set; }

        public DateTime ExpiresAt { get; private set; }

        public bool Revoked { get; private set; }

        private RefreshToken()
        {
        }

        public RefreshToken(
            string token,
            Guid userId,
            DateTime expiresAt)
        {
            Id = Guid.NewGuid();

            Token = token;

            UserId = userId;

            ExpiresAt = expiresAt;

            Revoked = false;
        }

        public void Revoke()
        {
            Revoked = true;
        }
    }
}
