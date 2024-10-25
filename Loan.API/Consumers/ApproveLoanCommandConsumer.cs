using MassTransit;
using Shared.Commands;
using Shared.Events;
using Shared;

namespace Loan.API.Consumers
{
    public class ApproveLoanCommandConsumer : IConsumer<ApproveLoanCommand>
    {
        readonly ISendEndpointProvider _sendEndpointProvider;
        public ApproveLoanCommandConsumer(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task Consume(ConsumeContext<ApproveLoanCommand> context)
        {
            ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{Endpoints.LoanStateMachine}"));
            //await sendEndpoint.Send(new LoanFaildEvent(context.Message.CorrelationId) { AccountNumber = context.Message.AccountNumber, Message = "İstenilen kredi miktari çok yüksek" });
            await sendEndpoint.Send(new LoanApprovedEvent(context.Message.CorrelationId) { AccountNumber = context.Message.AccountNumber });

        }
    }
}
