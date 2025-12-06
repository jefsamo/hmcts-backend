using HMCTS_Test.Data;
using HMCTS_Test.DTOs;
using HMCTS_Test.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HMCTS_Test.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _dbContext;

        public TaskService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskResponse> CreateTaskAsync(
     CreateTaskRequest request,
     CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<Models.TaskStatus>(request.Status, ignoreCase: true, out var status))
            {
                throw new ArgumentException(
                    "Invalid status. Allowed values: Todo, InProgress, Done.",
                    nameof(request.Status));
            }

            if (string.IsNullOrWhiteSpace(request.DueDateTime))
            {
                throw new ArgumentException(
                    "Due date/time is required.",
                    nameof(request.DueDateTime));
            }

            if (!DateTime.TryParse(
                    request.DueDateTime,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                    out var dueDateTimeUtc))
            {
                throw new ArgumentException(
                    "Invalid date format. Use ISO 8601 format e.g. 2025-12-06T14:30:00Z",
                    nameof(request.DueDateTime));
            }

            var task = new TaskItem
            {
                Title = request.Title.Trim(),
                Description = string.IsNullOrWhiteSpace(request.Description)
                    ? null
                    : request.Description.Trim(),

                Status = status,

                DueDateTime = dueDateTimeUtc
            };

            await _dbContext.Tasks.AddAsync(task, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                DueDateTime = task.DueDateTime
            };
        }


        public async Task<List<TaskResponse>> GetAllTasksAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Tasks
                .OrderBy(t => t.DueDateTime)
                .Select(t => new TaskResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status.ToString(),
                    DueDateTime = t.DueDateTime
                })
                .ToListAsync(cancellationToken);
        }
    }
}
