using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.BookListDtos
{
    public class AddBookToListDto
    {
        [Required]
        public int BookId { get; set; }
    }
}