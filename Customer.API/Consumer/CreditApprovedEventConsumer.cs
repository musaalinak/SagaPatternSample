using MassTransit;
using Shared.Events;

namespace Customer.API.Consumer
{
    public class CreditApprovedEventConsumer : IConsumer<CreditApprovedEvent>
    {
        private readonly ILogger<CreditApprovedEventConsumer> logger;

        public CreditApprovedEventConsumer(ILogger<CreditApprovedEventConsumer> logger)
        {
            this.logger = logger;
        }
        public Task Consume(ConsumeContext<CreditApprovedEvent> context)
        {
            this.logger.LogInformation($"Credit Approved recieved :{context.Message.AccountNumber}");
            return Task.CompletedTask;
        }
    }
}
