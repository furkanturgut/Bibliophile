using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class BookAuthorsOfPost
    {
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
        public int? PostId { get; set; }
        public BlogPost? BlogPost { get; set; }
    }
}