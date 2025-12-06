using System.ComponentModel.DataAnnotations;

namespace HMCTS_Test.DTOs
{
    public class CreateTaskRequest
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = "Todo";

        [Required]
        public string? DueDateTime { get; set; } = string.Empty;
    }
}
