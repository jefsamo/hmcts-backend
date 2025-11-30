using System.ComponentModel.DataAnnotations;

namespace HMCTS_Test.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Required]
        public TaskStatus Status { get; set; }

        [Required]
        public DateTime DueDateTime { get; set; }
    }
}
