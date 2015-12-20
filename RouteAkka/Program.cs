using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using RouteAkka.Actors;
using RouteAkka.ExternalSystem;
using RouteAkka.Messages;

namespace RouteAkka
{
    class Program
    {
        private static ActorSystem _system;

        static void Main(string[] args)
        {

            var container = Configure(new ContainerBuilder());

            _system = ActorSystem.Create("PaymentProcessing");

            IDependencyResolver resolver = new AutoFacDependencyResolver(container, _system);


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
