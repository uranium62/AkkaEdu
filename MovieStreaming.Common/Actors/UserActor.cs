using System;
using Akka.Actor;
using MovieStreaming.Common.Messages;

namespace MovieStreaming.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentWathing;

        public UserActor(int id)
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

                Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(movie.MovieTitle));

                Become(Playing);
            });

            Receive<StopMovieMessage>(signal =>
            {
                Console.WriteLine("[ERROR]: Can't stop if nothing to play");
            });
        }
    }
}
