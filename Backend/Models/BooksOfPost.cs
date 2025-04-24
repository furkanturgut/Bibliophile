using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class BooksOfPost
    {
        public int? BookId { get; set; }
        public Book? Book { get; set; }
        public int? PostId { get; set; }
        public BlogPost? Post { get; set; }
    }
}