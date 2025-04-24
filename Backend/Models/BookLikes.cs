using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class BookLikes
    {
        public int? BookId { get; set; }
        public Book? Book { get; set; }
        public int? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}