using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string FavouriteMovieGenre { get; private set; } = "Thriller";
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Movie> WatchLaterMovies { get; set; } = new List<Movie>();
        public ICollection<Movie> FavoriteListMovies { get; set; } = new List<Movie>();
    }
}
