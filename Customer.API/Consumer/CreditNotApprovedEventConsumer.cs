using MassTransit;
using Shared.Events;

namespace Customer.API.Consumer
{
    public class CreditNotApprovedEventConsumer : IConsumer<CreditNotApprovedEvent>
    {
        private readonly ILogger<CreditNotApprovedEventConsumer> logger;

        public CreditNotApprovedEventConsumer(ILogger<CreditNotApprovedEventConsumer> logger)
        {
            this.logger = logger;
        }
        public Task Consume(ConsumeContext<CreditNotApprovedEvent> context)
        {
            this.logger.LogInformation($"Credit Not Approved recieved :{context.Message.Message}");
            return Task.CompletedTask;
        }
    }
}
