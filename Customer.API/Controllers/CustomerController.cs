using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Commands;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ISendEndpointProvider _sendEndpointProvider;
        public CustomerController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }
        [HttpPost]
        public async Task<IActionResult> ApplyLoan()
        {

            ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new($"queue:{Endpoints.LoanStateMachine}"));

            Random rdm = new Random();
            int randomId = rdm.Next(1, 1000);

            LoanRequestCommand requestCommand = new LoanRequestCommand { AccountNumber = randomId, Amount = 10000 };

            await sendEndpoint.Send(requestCommand);

            return Ok();
        }
    }
}
