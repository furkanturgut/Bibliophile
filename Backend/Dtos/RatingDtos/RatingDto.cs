using System;

namespace Backend.Dtos.RatingDtos
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int Rate { get; set; } 
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
  
        public string UserId { get; set; }
        public string UserName { get; set; }
        

        public int BookId { get; set; }
        public string BookName { get; set; }
    }
}