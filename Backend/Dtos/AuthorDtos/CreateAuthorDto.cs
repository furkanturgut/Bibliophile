using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.AuthorDto
{
    /// <summary>
    /// Yeni bir yazar oluşturmak için kullanılan DTO
    /// </summary>
    public class CreateAuthorDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Surname { get; set; }
        
        [StringLength(2000)]
        public string Biography { get; set; }
        
        [StringLength(500)]
        public string AuthorPhoto { get; set; }
    }
}