using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ChangeUsernameDTO
    {
        public string UserId { get; set; }
        public string NewUsername { get; set; }
    }
}
