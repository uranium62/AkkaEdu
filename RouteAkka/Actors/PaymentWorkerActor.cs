using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using RouteAkka.ExternalSystem;
using RouteAkka.Messages;

namespace RouteAkka.Actors
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
