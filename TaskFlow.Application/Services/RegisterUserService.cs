using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs.Auth;
using TaskFlow.Domain.Constants;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Services
{
    public class RegisterUserService
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(RegisterUserRequest request)
        {
            // Check if user with the same email already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser is not null)
            {
                throw new Exception("User with this email already exists.");
            }

            // Hash the password 
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create new user entity
            var user = new User(
                request.Name,
                request.Email,
                passwordHash,
                Roles.User
                );
            // Save user to the database
            await _userRepository.AddUserAsync(user);
        }
    }
}
