using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;
using RouteAkka.Messages;

namespace RouteAkka.Actors
{
    class JobCoordinatorActor : ReceiveActor
    {
        private readonly IActorRef _paymentWorker;
        private int _numberOfRemainingPayments;

        public JobCoordinatorActor()
        {

           //_paymentWorker = Context.ActorOf(Props.Empty.WithRouter(new RoundRobinPool(5)));
           _paymentWorker = Context.ActorOf(Context.DI().Props<PaymentWorkerActor>().WithRouter(FromConfig.Instance), "PaymentWorkers");

            Receive<ProcessFileMessage>(msg =>
            {
                List<SendPaymentMessage> requests = ParseCsvFile(msg.FileName);

                _numberOfRemainingPayments = requests.Count();

                foreach (var sendPaymentMessage in requests)
                {
                    _paymentWorker.Tell(sendPaymentMessage);
                }
            });

            Receive<PaymentSentMessage>(
                message =>
                {
                    _numberOfRemainingPayments--;

                    var jobIsComplete = _numberOfRemainingPayments == 0;

                    if (jobIsComplete)
                    {
                        Context.System.Shutdown();
                    }
                });
        }

        private List<SendPaymentMessage> ParseCsvFile(string fileName)
        {
            var messagesToSend = new List<SendPaymentMessage>();

            var fileLines = File.ReadAllLines(fileName);

            foreach (var line in fileLines)
            {
                var values = line.Split(',');
                var format = new NumberFormatInfo();

                var message = new SendPaymentMessage(
                                    values[0],
                                    values[1],
                                    int.Parse(values[3]),
                                    decimal.Parse(values[2], format));

                messagesToSend.Add(message);
            }

            return messagesToSend;
        }
    }
}
