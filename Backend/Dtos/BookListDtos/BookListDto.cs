using System;
using System.Collections.Generic;

namespace Backend.Dtos.BookListDtos
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Kullanıcı bilgileri
        public string UserId { get; set; }
        public string UserName { get; set; }
        
        // Liste bilgileri
        public int BooksCount { get; set; }
        public int LikesCount { get; set; }
        
        // Listedeki kitaplar
        public List<BookInListDto>? Books { get; set; }
    }

    public class BookInListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
    }
}