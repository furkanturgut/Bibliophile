using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Rating>? Ratings { get; set; }
        public List<BookLikes>? BookLikes { get; set; }
        public List<BookList>? BookLists { get; set; }
        public List<ListLike>? ListLikes { get; set; }
        public List<BlogPost>? BlogPosts { get; set; }
        public List<PostLike>? PostLikes { get; set; }
    }
}