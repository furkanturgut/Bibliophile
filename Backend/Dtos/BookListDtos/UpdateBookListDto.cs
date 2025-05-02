using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.BookListDtos
{
    public class UpdateBookListDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
    }
}