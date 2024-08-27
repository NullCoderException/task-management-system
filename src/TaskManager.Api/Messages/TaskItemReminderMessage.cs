
namespace TaskManager.Api.Messages;
public class TaskItemReminderMessage
{
    public int TaskId { get; set; }
    public string TaskTitle { get; set; }
    public DateTime DueDate { get; set; }
}