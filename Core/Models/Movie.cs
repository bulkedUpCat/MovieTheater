using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseDate { get; set; }
        public string Director { get; set; }
        public double RuntimeHours { get; set; }
        public string Image { get; set; }
        public ICollection<MovieGenre> Genres { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<User> WatchLaterUsers { get; set; } = new List<User>();
        public ICollection<User> FavoriteListUsers { get; set; } = new List<User>();
    }
}
