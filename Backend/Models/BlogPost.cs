using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }
        public AppUser? User { get; set; }
        public List<PostLike>? PostLikes { get; set; }
        public List<BookAuthorsOfPost>? BookAuthorsOfPost { get; set; }
        public List<BooksOfPost> BooksOfPost { get; set; }
    }
}