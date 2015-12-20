using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SImpleAkka
{
    public class PlayMovieMessage
    {
        public string MovieTitle { get; private set; }

        public int UserId { get; private set; }

        public PlayMovieMessage(string title, int user)
        {
            MovieTitle = title;
            UserId = user;
        }
    }
}
