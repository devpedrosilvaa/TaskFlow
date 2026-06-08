using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlow.Application.DTOs.Task;
using TaskFlow.Application.Services;
using TaskFlow.Domain.Constants;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _service;

        public TasksController(TaskService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Create(
            CreateTaskRequest request)
        {
            var userId =
                Guid.Parse(
                    User.FindFirst(
                        ClaimTypes.NameIdentifier)!
                    .Value);

            await _service.CreateAsync(
                request,
                userId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var role =
                User.FindFirst(
                    ClaimTypes.Role)?.Value;

            var userId =
                Guid.Parse(
                    User.FindFirst(
                        ClaimTypes.NameIdentifier)!
                    .Value);

            if (role == Roles.Admin)
            {
                return Ok(
                    await _service.GetAllAsync());
            }

            return Ok(
                await _service.GetByUserAsync(
                    userId));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(
            Guid id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
