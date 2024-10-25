using MassTransit;
using Shared.Events;

namespace Customer.API.Consumer
{
    public class LoanFaildEventConsumer : IConsumer<LoanFaildEvent>
    {
        private readonly ILogger<LoanFaildEventConsumer> logger;

        public LoanFaildEventConsumer(ILogger<LoanFaildEventConsumer> logger)
        {
            this.logger = logger;
        }
        public Task Consume(ConsumeContext<LoanFaildEvent> context)
        {
            this.logger.LogInformation($"Loan Failed recieved :{context.Message.Message}");
            return Task.CompletedTask;
        }
    }
}
