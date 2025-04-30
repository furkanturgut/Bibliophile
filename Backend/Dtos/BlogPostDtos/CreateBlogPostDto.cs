using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.BlogPostDtos
{
    public class CreateBlogPostDto
    {
        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        // İlişkili kitap ve yazarlar için ID'ler
        public List<int>? BookIds { get; set; }
        public List<int>? AuthorIds { get; set; }
    }
}