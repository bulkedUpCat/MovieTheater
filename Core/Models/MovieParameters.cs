using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MovieParameters
    {
        const int _maxPageSize = 12;
        public int PageNumber { get; set; } = 1;
        int _pageSize = 6;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set 
            {
                _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
            } 
        }

        public List<string> Genres { get; set; } = new List<string>();
        public List<string> Years { get; set; } = new List<string>();
    }
}
