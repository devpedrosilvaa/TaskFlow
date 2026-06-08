using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<TaskItem> Tasks { get; private set; }
            = new List<TaskItem>();

        private User()
        {
        }

        public User(
        string name,
        string email,
        string passwordHash,
        string role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
