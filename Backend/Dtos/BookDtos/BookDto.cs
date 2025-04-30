using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos.BookDtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string BookCover { get; set; }
        public string Summary { get; set; }
        public decimal AverageRating { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public List<string>? GenreNames { get; set; }
        public int LikesCount { get; set; }
    }
}