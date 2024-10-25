using MassTransit;
using Shared.Events;

namespace Customer.API.Consumer
{
    public class LoanApprovedEventConsumer : IConsumer<LoanApprovedEvent>
    {
        private readonly ILogger<LoanApprovedEventConsumer> logger;

        public LoanApprovedEventConsumer(ILogger<LoanApprovedEventConsumer> logger)
        {
            this.logger = logger;
        }
        public Task Consume(ConsumeContext<LoanApprovedEvent> context)
        {
            this.logger.LogInformation($"Loan Approved recieved :{context.Message.AccountNumber}");
            return Task.CompletedTask;
        }
    }
}
