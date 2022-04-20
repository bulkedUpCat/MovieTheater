using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CommentDTO
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Text { get; set; }
    }
}
