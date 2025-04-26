using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Biography { get; set; }
        public string AuthorPhoto { get; set; }
        public List<Book>? Books { get; set; }
        public List<BookAuthorsOfPost>? BookAuthorsOfPost { get; set; }

    }
}