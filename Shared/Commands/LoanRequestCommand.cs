using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commands
{
    public class LoanRequestCommand 
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
