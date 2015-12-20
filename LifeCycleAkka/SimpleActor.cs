using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace LifeCycleAkka
{
    class SimpleActor : ReceiveActor
    {

        public SimpleActor()
        {
            Receive<string>(msg =>
            {
                Console.WriteLine(msg);
            });
        }

        protected override void PreStart()
        {
            Console.WriteLine("Pre Start");
        }

        protected override void PostStop()
        {
            Console.WriteLine("Post Stop");
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("Post Restart");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("Pre Restart");
        }
    }
}
