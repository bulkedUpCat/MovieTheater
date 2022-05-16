using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseDate { get; set; }
        public string Director { get; set; }
        public double RuntimeHours { get; set; }
        public string Image { get; set; }
        public string TrailerUrl { get; set; }
        public IEnumerable<MovieGenreDTO> Genres { get; set; }
        public ICollection<UserDTO> WatchLaterUsers { get; set; }
        public ICollection<UserDTO> FavoriteListUsers { get; set; }
    }
}
