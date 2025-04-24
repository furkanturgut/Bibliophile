using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class BooksOfList
    {
        public int? ListId { get; set; }
        public BookList? List { get; set; }
        public int? BookId { get; set; }
        public Book? Book { get; set; }
    }
}