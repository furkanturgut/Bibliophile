using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public required string BookCover { get; set; }
        public required string Summary { get; set; }
        public decimal AvarageRating { get; set; }
        public int? AuthorId { get; set; } 
        public Author? Author { get; set; }  
        public List<BookGenre>? BookGenres { get; set; }
        public List<BookLikes>? BookLikes { get; set; }
        public List<BooksOfList>? BooksOfLists { get; set; }
        public List<BooksOfPost>? BooksOfPosts { get; set; }
        public List<Rating>? Ratings { get; set; }

    }
}