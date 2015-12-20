using Akka.Actor;

namespace MovieStreaming.Remote
{
    class Program
    {
        private static ActorSystem _movieStreamingActorSystem;

        static void Main(string[] args)
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            _movieStreamingActorSystem.AwaitTermination();
        }
    }
}
