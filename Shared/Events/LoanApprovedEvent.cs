using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class LoanApprovedEvent : CorrelatedBy<Guid>
    {

        public LoanApprovedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;

        }
        public Guid CorrelationId { get; }

        public int AccountNumber { get; set; }
    }
}
