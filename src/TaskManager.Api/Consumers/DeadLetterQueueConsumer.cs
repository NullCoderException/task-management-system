using MassTransit;
using Microsoft.Extensions.Logging;

namespace TaskManager.Api.Consumers
{
    public class DeadLetterQueueConsumer : IConsumer<ReceiveContext>
    {
        private readonly ILogger<DeadLetterQueueConsumer> _logger;

        public DeadLetterQueueConsumer(ILogger<DeadLetterQueueConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ReceiveContext> context)
        {
            var originalMessage = context.Message;
            var exception = context.ReceiveContext.TransportHeaders.Get<Exception>("MT-Exception-Message");

            _logger.LogError(exception, "Message sent to dead-letter queue: {MessageId}", originalMessage.GetMessageId());

            // Here you can implement logic to handle the dead-lettered message
            // For example, you might want to store it in a database for later analysis
        }
    }
}