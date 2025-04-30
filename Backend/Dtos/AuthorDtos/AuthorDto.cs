using System;
using System.Collections.Generic;

namespace Backend.Dtos.AuthorDto
{
    /// <summary>
    /// Yazarın görüntülenmesi için kullanılan DTO
    /// </summary>
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Biography { get; set; }
        public string AuthorPhoto { get; set; }
        
        // İlişkili kitapların isimleri
        public List<string>? BookNames { get; set; }
        
    }
}