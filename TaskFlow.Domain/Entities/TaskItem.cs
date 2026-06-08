using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private TaskItem()
        {
        }

        public TaskItem(
            string title,
            string description,
            Guid userId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

    }
}
