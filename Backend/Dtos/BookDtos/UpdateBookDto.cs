using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos.BookDtos
{
    public class UpdateBookDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}