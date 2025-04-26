using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class ListLike
    {
        public int? ListId { get; set; }
        public BookList? List { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}