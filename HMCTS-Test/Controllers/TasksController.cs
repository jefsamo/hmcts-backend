using HMCTS_Test.DTOs;
using HMCTS_Test.Services;
using Microsoft.AspNetCore.Mvc;

namespace HMCTS_Test.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        /// <summary>Create a new task.</summary>
        /// <response code="201">Task created successfully.</response>
        /// <response code="400">Validation error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TaskResponse>> CreateTask([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var created = await _taskService.CreateTaskAsync(request, cancellationToken);
                return CreatedAtAction(nameof(CreateTask), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while creating task");
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
