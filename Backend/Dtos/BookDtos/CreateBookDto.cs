using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos
{
    public class CreateBookDto
    {
        [Required]
        [StringLength(200)]
        public  required string Name { get; set; }
        
        [Required]
        public DateOnly ReleaseDate { get; set; }
        
        [StringLength(500)]
        public required string BookCover { get; set; }
        
        [StringLength(2000)]
        public required string Summary { get; set; }
        
        [Required]
        public int AuthorId { get; set; }

        public List<int>? GenreIds { get; set; }
    }
}