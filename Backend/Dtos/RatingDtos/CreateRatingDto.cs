using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.RatingDtos
{
    public class CreateRatingDto
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Değerlendirme 1-5 arasında olmalıdır.")]
        public int Rate { get; set; }
        
        [StringLength(500, ErrorMessage = "Yorum en fazla 500 karakter olabilir.")]
        public string Comment { get; set; }
        
        [Required]
        public int BookId { get; set; }
    }
}