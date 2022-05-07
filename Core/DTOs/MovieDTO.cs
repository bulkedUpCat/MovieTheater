using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public double Runtime { get; set; }
        public IEnumerable<string> Genres { get; set; }
    }
}
