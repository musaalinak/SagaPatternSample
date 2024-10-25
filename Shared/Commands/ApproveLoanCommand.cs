using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commands
{
    public class ApproveLoanCommand : CorrelatedBy<Guid>
    {
        public ApproveLoanCommand(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid CorrelationId { get; }

        public int AccountNumber { get; set; }
    }
}
