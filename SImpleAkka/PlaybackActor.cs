using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace SImpleAkka
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Receive<PlayMovieMessage>((movie) =>
            {
                Console.WriteLine("title: {0}, user: {1}", movie.MovieTitle, movie.UserId);
            });
        }
    }
}
