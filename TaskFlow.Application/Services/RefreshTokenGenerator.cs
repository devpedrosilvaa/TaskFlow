using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TaskFlow.Application.Services
{
    public class RefreshTokenGenerator
    {
        public static string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            using var rng =
                RandomNumberGenerator.Create();

            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(
                randomBytes);
        }
    }
}
