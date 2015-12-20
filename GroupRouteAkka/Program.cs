using System;
using System.Diagnostics;
using System.Linq;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using GroupRouteAkka.Actors;
using GroupRouteAkka.ExternalSystem;
using GroupRouteAkka.Messages;

namespace GroupRouteAkka
{
    class Program
    {
        private static ActorSystem _system;

        static void Main(string[] args)
        {

            var container = Configure(new ContainerBuilder());

            _system = ActorSystem.Create("PaymentProcessing");

            IDependencyResolver resolver = new AutoFacDependencyResolver(container, _system);

            _system.ActorOf(_system.DI().Props<PaymentWorkerActor>(), "PaymentWorker1");
            _system.ActorOf(_system.DI().Props<PaymentWorkerActor>(), "PaymentWorker2");
            _system.ActorOf(_system.DI().Props<PaymentWorkerActor>(), "PaymentWorker3");


            IActorRef jobCoordinator = _system.ActorOf<JobCoordinatorActor>("JobCoordinator");

            var jobTime = Stopwatch.StartNew();

            jobCoordinator.Tell(new ProcessFileMessage("data.csv"));

            _system.AwaitTermination();

            jobTime.Stop();

            Console.WriteLine("Job complete in {0}ms ", jobTime.ElapsedMilliseconds);
            Console.ReadLine();
        }

        static IContainer Configure(ContainerBuilder builder)
        {
            builder.RegisterType<DemoPaymentGateway>().As<IPaymentGateway>();
            builder.RegisterType<PaymentWorkerActor>();

            return builder.Build();
        }
    }
}
