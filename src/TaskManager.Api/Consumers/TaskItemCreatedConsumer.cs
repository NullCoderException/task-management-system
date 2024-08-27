using MassTransit;
using Microsoft.Extensions.Logging;
using TaskManager.Api.Messages;

namespace TaskManager.Api.Consumers
{
    public class TaskItemCreatedConsumer : IConsumer<TaskItemCreatedMessage>
    {
        private readonly ILogger<TaskItemCreatedConsumer> _logger;

        public TaskItemCreatedConsumer(ILogger<TaskItemCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<TaskItemCreatedMessage> context)
        {
            _logger.LogInformation("TaskCreatedConsumer: Starting to process message");
            
            try
            {
                var message = context.Message;
                _logger.LogInformation("Received TaskCreatedMessage for TaskItem: Id = {TaskItemId}, Title = {TaskItemTitle}", 
                    message.Id, message.Title);

                // Here you can add any additional processing logic

                _logger.LogInformation("TaskCreatedConsumer: Finished processing message");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskCreatedConsumer: Error processing message");
                throw;
            }

        
        }
    }
}