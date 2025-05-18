using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Interface;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDataContext _datacontext;

        public BookRepository(ApplicationDataContext datacontext)
        {
            this._datacontext = datacontext;
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            try
            {
                _datacontext.books.AddAsync(book);
                await _datacontext.SaveChangesAsync();
                return book;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Book?> DeleteBookAsync(Book book)
        {
            try
            {
                _datacontext.books.Remove(book);
                await _datacontext.SaveChangesAsync();
                return book;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Book?>> GetAllBooksAsync()
        {
            try
            {
                return await _datacontext.books
                    .Include(b => b.BookGenres)
                        .ThenInclude(bg => bg.Genre) 
                    .ToListAsync();
                 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            try
            {
               return await _datacontext.books
                    .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre) 
                    .Where(b => b.Id == id)
                    .FirstOrDefaultAsync();
                
            }
            catch (Exception)  
            {
                throw;
            }
        }

        public async Task<List<Book?>> GetBooksByAuthorAsync(Author author)
        {
            try
            {
                
                 return await _datacontext.books.Where(b => b.Author== author).ToListAsync();
               
              
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Book?>> GetBooksByGenreAsync(Genre genre)
        {
            try
            {
            return await _datacontext.books
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Where(b => b.BookGenres.Any(bg => bg.Genre.Id == genre.Id))
                .ToListAsync();
        }
        catch (Exception)
        {
            throw;
            }
        }
        public async Task<Book?> UpdateBookAsync(Book book)
        {
            try
            {
                _datacontext.books.Update(book);
                await _datacontext.SaveChangesAsync();
                return book;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}