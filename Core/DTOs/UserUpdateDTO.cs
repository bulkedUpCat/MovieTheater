using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserUpdateDTO
    {
        public string Name { get; set; }
        public ICollection<Movie> WatchLaterMovies { get; set; }
        public ICollection<Movie> FavoriteMovies { get; set; }
    }
}
