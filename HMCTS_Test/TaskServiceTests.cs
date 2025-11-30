using HMCTS_Test.Data;
using HMCTS_Test.DTOs;
using HMCTS_Test.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMCTS_Test
{

    public class TaskServiceTests
    {
        private static AppDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateTaskAsync_ValidRequest_CreatesTask()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var service = new TaskService(context);

            var request = new CreateTaskRequest
            {
                Title = "Test task",
                Description = "Unit test",
                Status = "Todo",
                DueDateTime = DateTime.UtcNow.AddDays(1)
            };

            // Act
            var result = await service.CreateTaskAsync(request);

            // Assert
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(request.Title, result.Title);
            Assert.Equal("Todo", result.Status);
            Assert.Single(context.Tasks);
        }

        [Fact]
        public async Task CreateTaskAsync_InvalidStatus_ThrowsArgumentException()
        {
            using var context = CreateInMemoryContext();
            var service = new TaskService(context);

            var request = new CreateTaskRequest
            {
                Title = "Bad status",
                Status = "INVALID",
                DueDateTime = DateTime.UtcNow.AddDays(1)
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTaskAsync(request));
        }

        [Fact]
        public async Task CreateTaskAsync_NullDueDate_ThrowsArgumentException()
        {
            using var context = CreateInMemoryContext();
            var service = new TaskService(context);

            var request = new CreateTaskRequest
            {
                Title = "No due date",
                Status = "Todo",
                DueDateTime = null
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTaskAsync(request));
        }
    }
}
