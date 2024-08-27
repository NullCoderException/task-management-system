using MassTransit;
using TaskManager.Api.Messages;

namespace TaskManager.Api.Consumers;

public class TaskReminderConsumer : IConsumer<TaskItemReminderMessage>
{
    private readonly ILogger<TaskReminderConsumer> _logger;

    public TaskReminderConsumer(ILogger<TaskReminderConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TaskItemReminderMessage> context)
    {
        var message = context.Message;
        _logger.LogInformation("Reminder for task: {TaskId} - {TaskTitle}. Due date: {DueDate}", 
            message.TaskId, message.TaskTitle, message.DueDate);

        // Here you would implement the logic to send a reminder
        // For example, sending an email or a push notification

    }
}
