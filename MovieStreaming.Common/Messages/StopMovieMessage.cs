namespace MovieStreaming.Common.Messages
{
    public class StopMovieMessage
    {
        public int UserId { get; private set; }

        public StopMovieMessage(int user)
        {
            UserId = user;
        }
    }
}
