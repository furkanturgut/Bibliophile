using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Dtos.BookDtos;
using Backend.Models;

namespace Backend.Interface
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        
        Task<List<BookDto?>> GetBooksByAuthorAsync(int author);
        Task<List<BookDto?>> GetBooksByGenreAsync(int genreId);
        Task<BookDto?> AddBookAsync(CreateBookDto book, string userName);
        Task<BookDto?> UpdateBookAsync(UpdateBookDto book, int Id, string userName);
        Task<BookDto?> DeleteBookAsync(int id, string userName);
    }
    }
