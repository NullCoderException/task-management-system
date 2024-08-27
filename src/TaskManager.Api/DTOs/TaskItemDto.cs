using System;

namespace TaskManager.Api.DTOs
{
    public class TaskItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}