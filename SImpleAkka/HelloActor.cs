using System;
using Akka.Actor;

namespace SImpleAkka
{
    public class HelloActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Console.WriteLine(message as string);
        }
    }
}
