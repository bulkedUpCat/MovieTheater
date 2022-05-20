using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Movie : BaseEntity
    {
        [Column(TypeName = "varchar(30)")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(2000)")]
        public string Description { get; set; }

        public int ReleaseDate { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Director { get; set; }

        [Column(TypeName = "varchar(30)")]
        public double RuntimeHours { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Image { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string TrailerUrl { get; set; }

        public DateTime WhenAdded { get; set; } = DateTime.Now;
        public virtual ICollection<MovieGenre> Genres { get; set; } = new List<MovieGenre>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<User> WatchLaterUsers { get; set; } = new List<User>();
        public ICollection<User> FavoriteListUsers { get; set; } = new List<User>();
    }
}
