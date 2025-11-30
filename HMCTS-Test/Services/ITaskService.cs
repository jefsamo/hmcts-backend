using HMCTS_Test.DTOs;

namespace HMCTS_Test.Services
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request, CancellationToken cancellationToken = default);
    }
}
