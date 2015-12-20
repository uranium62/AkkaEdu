using System;
using Akka.Actor;

namespace BehaviorAkka
{
    class UserActor : ReceiveActor
    {
        private string _currentWathing;

        public UserActor()
        {
            Stopped();
        }


        private void Playing()
        {
            Receive<PlayMovieMessage>(movie =>
            {
                Console.WriteLine("[ERROR]: Can't start playing another movie before stopping existing one");

            });

            Receive<StopMovieMessage>(signal =>
            {
                Console.WriteLine("User has stopped watching {0}", _currentWathing);
                _currentWathing = null;

                Become(Stopped);
            });
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(movie =>
            {
                _currentWathing = movie.MovieTitle;
                Console.WriteLine("User: {0} is currentrly watching {1}", movie.UserId, movie.MovieTitle);

                Become(Playing);
            });

            Receive<StopMovieMessage>(signal =>
            {
                Console.WriteLine("[ERROR]: Can't stop if nothing to play");
            });
        }
    }
}
