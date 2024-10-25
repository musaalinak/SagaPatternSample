using MassTransit;
using Shared;
using Shared.Commands;
using Shared.Events;

namespace Credit.API.Consumers
{
    public class CheckCreditScoreCommandConsumer : IConsumer<CheckCreditScoreCommand>
    {
        readonly ISendEndpointProvider _sendEndpointProvider;
        public CheckCreditScoreCommandConsumer(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task Consume(ConsumeContext<CheckCreditScoreCommand> context)
        {
            ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{Endpoints.LoanStateMachine}"));

            if (context.Message.Amount > 1000000)
            {
                await sendEndpoint.Send(new CreditNotApprovedEvent(context.Message.CorrelationId) { AccountNumber = context.Message.AccountNumber, Message = "İstenilen kredi miktari çok yüksek" });
            }
            else { 
                await sendEndpoint.Send(new CreditApprovedEvent(context.Message.CorrelationId) { AccountNumber=context.Message.AccountNumber });
            }
        }
    }
}
