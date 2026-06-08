using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs.Task;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateTaskRequest request, Guid userId)
        {
            var task = new TaskItem
            (
                request.Title,
                request.Description,
                userId
            );
            await _repository.AddAsync(task);
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<TaskItem>> GetByUserAsync(Guid userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            if(task is null)
                throw new Exception("Task not found");

            await _repository.DeleteAsync(task);
        }
    }
}
