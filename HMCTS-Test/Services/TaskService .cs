using HMCTS_Test.Data;
using HMCTS_Test.DTOs;
using HMCTS_Test.Models;

namespace HMCTS_Test.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _dbContext;

        public TaskService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<Models.TaskStatus>(request.Status, ignoreCase: true, out var status))
            {
                throw new ArgumentException("Invalid status. Allowed values: Todo, InProgress, Done.", nameof(request.Status));
            }

            if (request.DueDateTime is null)
            {
                throw new ArgumentException("Due date/time is required.", nameof(request.DueDateTime));
            }

            var task = new TaskItem
            {
                Title = request.Title.Trim(),
                Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim(),
                Status = status,
                DueDateTime = DateTime.SpecifyKind(request.DueDateTime.Value, DateTimeKind.Utc)
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
    }
}
