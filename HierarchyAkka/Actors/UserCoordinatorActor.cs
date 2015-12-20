using System.Collections.Generic;
using Akka.Actor;
using HierarchyAkka.Messages;

namespace HierarchyAkka.Actors
{
    class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(msg =>
            {
                IActorRef child = CreateChildIfNotExists(msg.UserId);

                child.Tell(msg);
            });

            Receive<StopMovieMessage>(msg =>
            {
                IActorRef child = CreateChildIfNotExists(msg.UserId);

                child.Tell(msg);
            });
        }


        private  IActorRef CreateChildIfNotExists(int userid)
        {
            IActorRef child;

            if (!_users.TryGetValue(userid, out child))
            {
                child = Context.ActorOf(Props.Create<UserActor>(userid), "User" + userid);
                _users.Add(userid, child);
            }

            return child;
        }
    }
}
