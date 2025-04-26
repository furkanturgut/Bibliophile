using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public int? BookId { get; set; }
        public Book? Book { get; set; }
    }
}