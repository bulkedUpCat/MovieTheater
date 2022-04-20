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
        public IEnumerable<string> Genre { get; set; }
        public int YearReleased { get; set; }
    }
}
