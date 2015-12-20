using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace SImpleAkka
{
    class Program
    {
        private static ActorSystem _actorSystem;

        static void Main(string[] args)
        {
            _actorSystem = ActorSystem.Create("simple");

            IActorRef hello = _actorSystem.ActorOf(Props.Create(() => new HelloActor()), "hello");
            IActorRef playback = _actorSystem.ActorOf(Props.Create(() => new PlaybackActor()), "playback");

            hello.Tell("Hello world");

            playback.Tell(new PlayMovieMessage("Mad Max", 1));
            playback.Tell(new PlayMovieMessage("Mad Max", 2));
            playback.Tell("movie");

            Console.ReadKey();

            _actorSystem.Shutdown();
        }
    }
}
