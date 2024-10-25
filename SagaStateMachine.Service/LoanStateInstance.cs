using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaStateMachine.Service
{
    public class LoanStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } // her bir asenkron işlemdeki state bilgisi
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }

    }
}
