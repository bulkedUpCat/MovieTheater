using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Comment : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public string Text { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("d");
    }
}
