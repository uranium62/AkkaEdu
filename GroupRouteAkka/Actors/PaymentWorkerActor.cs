using System;
using Akka.Actor;
using GroupRouteAkka.ExternalSystem;
using GroupRouteAkka.Messages;

namespace GroupRouteAkka.Actors
{
    class PaymentWorkerActor : ReceiveActor
    {
        private readonly IPaymentGateway _paymentGateway;

        public PaymentWorkerActor(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;

            Receive<SendPaymentMessage>(msg =>
            {
                Console.WriteLine("Sending payment for {0} {1}", msg.FirstName, msg.LastName);

                _paymentGateway.Pay(msg.AccountNumber, msg.Amount);

                Sender.Tell(new PaymentSentMessage(msg.AccountNumber));
            });
        }
    }
}
