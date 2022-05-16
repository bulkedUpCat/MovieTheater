using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class MovieException : Exception
    {
        public MovieException() : base() { }
        public MovieException(string message) : base(message) { }
        public MovieException(string message, Exception inner) : base(message, inner) { }
    }
}
