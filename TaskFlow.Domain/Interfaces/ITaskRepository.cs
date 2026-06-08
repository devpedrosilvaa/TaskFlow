using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);

        Task<List<TaskItem>> GetByUserIdAsync(
            Guid userId);

        Task<List<TaskItem>> GetAllAsync();

        Task<TaskItem?> GetByIdAsync(Guid id);

        Task UpdateAsync(TaskItem task);

        Task DeleteAsync(TaskItem task);
    }
}
