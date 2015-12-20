using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using HierarchyAkka.Actors;
using HierarchyAkka.Messages;

namespace HierarchyAkka
{
    class Program
    {
        private static ActorSystem _system;

        static void Main(string[] args)
        {
            _system = ActorSystem.Create("StreamingMoviesSystem");

            _system.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            while (true)
            {
                Thread.SpinWait(100);

                Console.WriteLine("Enter a command: ");

                var cmd = Console.ReadLine() ?? "exit";

                if (cmd.StartsWith("play"))
                {
                    var temp = cmd.Split(',');
                    
                    _system.ActorSelection("/user/Playback/UserCoordinator")
                        .Tell(new PlayMovieMessage(temp[2], int.Parse(temp[1])));
                }

                if (cmd.StartsWith("stop"))
                {
                    var temp = cmd.Split(',');

                    _system.ActorSelection("/user/Playback/UserCoordinator")
                        .Tell(new StopMovieMessage(int.Parse(temp[1])));
                }

                if (cmd == "exit")
                {
                    _system.Shutdown();
                    _system.AwaitTermination();

                    break;
                }
            }
        }
    }
}
