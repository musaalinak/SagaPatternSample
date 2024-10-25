using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commands
{
    public class CheckCreditScoreCommand : CorrelatedBy<Guid>
    {
        public CheckCreditScoreCommand(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid CorrelationId { get; }

        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }

    }
}
