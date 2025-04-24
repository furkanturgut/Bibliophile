using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class PostLike
    {
        public int? UserId { get; set; }
        public AppUser? User { get; set; }
        public int? PostId { get; set; }
        public BlogPost Post { get; set; }

    }
}