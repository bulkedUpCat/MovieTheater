using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class AddMovieDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseDate { get; set; }
        public string Director { get; set; }
        public double RuntimeHours { get; set; }
        public string Image { get; set; }
        public string TrailerUrl { get; set; }
        public IEnumerable<string> Genres { get; set; }
    }
}
