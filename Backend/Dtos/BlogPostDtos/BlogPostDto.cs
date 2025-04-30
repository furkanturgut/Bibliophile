using System;
using System.Collections.Generic;
using Backend.Models;

namespace Backend.Dtos.BlogPostDtos
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Kullanıcı/yazar bilgileri
        public string UserId { get; set; }
        public string UserName { get; set; }
        
        // İlişkili veriler
        public List<string>? BookNames { get; set; }
        public List<string>? AuthorNames { get; set; }
        
    }
}