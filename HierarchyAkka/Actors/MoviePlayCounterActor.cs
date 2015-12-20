using System;
using System.Collections.Generic;
using Akka.Actor;
using HierarchyAkka.Exceptions;
using HierarchyAkka.Messages;

namespace HierarchyAkka.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message =>
            {
                if (_moviePlayCounts.ContainsKey(message.MovieTitle))
                {
                    _moviePlayCounts[message.MovieTitle]++;
                }
                else
                {
                    _moviePlayCounts.Add(message.MovieTitle, 1);
                }

                //  Simulated bugs
                if (_moviePlayCounts[message.MovieTitle] > 3)
                {
                    throw new SimulatedCorruptStateException();
                }

                if (message.MovieTitle == "Partial Recoil")
                {
                    throw new SimulatedTerribleMovieException();
                }

                Console.WriteLine(
                    "MoviePlayCounterActor '{0}' has been watched {1} times",
                    message.MovieTitle, _moviePlayCounts[message.MovieTitle]);
            });
        }
    }
}
