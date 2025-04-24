using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class BookList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public AppUser? User { get; set; }
        public List<ListLike>? ListLikes { get; set; }
        public List<BooksOfList>? BooksOfLists { get; set; }
    }
}