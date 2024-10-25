﻿using MassTransit;
using Shared.Events;
using Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Commands;

namespace SagaStateMachine.Service
{
    public class LoanStateMachine : MassTransitStateMachine<LoanStateInstance>
    {
        public Event<LoanRequestedEvent> LoanRequested { get; set; }
        public Event<CreditNotApprovedEvent> CreditNotApproved { get; set; }
        public Event<CreditApprovedEvent> CreditApproved { get; set; }
        public Event<LoanApprovedEvent> LoanApproved { get; set; }
        public Event<LoanFaildEvent> LoanFaild { get; set; }

        public State LoanRequestCommandState { get; set; }
        public State CreditNotApprovedState { get; set; }
        public State CreditApprovedState { get; set; }
        public State LoanApprovedState { get; set; }
        public State LoanFaildState { get; set; }

        public LoanStateMachine()
        {
            InstanceState(instance => instance.CurrentState);

            Event(() => LoanRequested, x => x.CorrelateBy(state => state.AccountNumber.ToString(), context => context.Message.AccountNumber.ToString()).SelectId(s => Guid.NewGuid()));

            Event(() => CreditNotApproved,
                orderStateInstance =>
                orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

            Event(() => CreditApproved,
                orderStateInstance =>
                orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

            Event(() => LoanApproved,
                orderStateInstance =>
                orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

            Event(() => LoanFaild,
                orderStateInstance =>
                orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

            Initially(When(LoanRequested).Send(new Uri($"queue:{Endpoints.CheckCreditScore}"), context => new CheckCreditScoreCommand(context.Saga.CorrelationId)
            {
                AccountNumber = context.Message.AccountNumber,
                Amount = context.Message.Amount
            }));

            During(LoanRequestCommandState,
                When(CreditApproved)
                .TransitionTo(CreditApprovedState)
                .Send(new Uri($"queue:{Endpoints.ApproveLoan}"), context => new ApproveLoanCommand(context.Saga.CorrelationId)
                {
                    AccountNumber = context.Message.AccountNumber,
                }),
                When(CreditNotApproved)
                .TransitionTo(CreditNotApprovedState)
                .Publish(context => new CreditNotApprovedEvent(context.Message.CorrelationId)
                {
                    AccountNumber = context.Message.AccountNumber,
                    Message = context.Message.Message
                }));
        }
    }
}