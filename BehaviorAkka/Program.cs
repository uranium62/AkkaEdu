using System;
using Akka.Actor;

namespace BehaviorAkka
{
    class Program
    {
        private static ActorSystem _streamingSystem;

        static void Main(string[] args)
        {
            _streamingSystem = ActorSystem.Create("MovingStreamingSystem");

            var play = _streamingSystem.ActorOf(Props.Create<UserActor>());

            Console.ReadKey();
            play.Tell(new PlayMovieMessage("film 1", 1));

            Console.ReadKey();
            play.Tell(new PlayMovieMessage("film 2", 1));

            Console.ReadKey();
            play.Tell(new StopMovieMessage());

            Console.ReadKey();
            play.Tell(new StopMovieMessage());

            Console.ReadKey();
            _streamingSystem.Shutdown();

        }
    }
}
