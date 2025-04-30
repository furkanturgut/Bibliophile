using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IAuthorRepository
    {
        Task<Author?> GetAuthorById(int id);
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> AddAuthorAsync(Author author);
        Task<Author?> UpdateAuthorAsync(Author author);
        Task<Author?> DeleteAuthorAsync(Author author);
        Task<bool> AuthorExistsAsync(int id);
    }
}