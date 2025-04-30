using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<List<Book?>> GetBooksByAuthorAsync(Author author);
        Task<List<Book?>> GetBooksByGenreAsync(Genre genreId);
        Task<Book?> AddBookAsync(Book book);
        Task<Book?> UpdateBookAsync(Book book);
        Task<Book?> DeleteBookAsync(Book book);
    }
}