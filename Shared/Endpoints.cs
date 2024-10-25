using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Endpoints
    {
        public const string LoanStateMachine = "loan-state-machine-queue";

        public const string CheckCreditScore = "check-credit-score-queue";
        public const string CreditApprovedEvent = "credit-approved-queue";
        public const string CreditNotApprovedEvent = "credit-notapproved-queue";

        public const string ApproveLoan = "approve-loan-queue";
        public const string LoanApprovedEvent = "loan-approved-queue";
        public const string LoanFaildEvent = "loan-failed-queue";
    }
}
