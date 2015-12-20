using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace LifeCycleAkka
{
    class Program
    {
        private static ActorSystem _system;

        static void Main(string[] args)
        {
            _system = ActorSystem.Create("test");

            var simple = _system.ActorOf(Props.Create(() => new SimpleActor()));
            simple.Tell("hi");

            Console.ReadKey();
            _system.Shutdown();
            Console.ReadKey();
        }
    }
}
