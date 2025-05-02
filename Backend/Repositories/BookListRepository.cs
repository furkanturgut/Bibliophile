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
    public class BookListRepository : IBookListRepository
    {
        private readonly ApplicationDataContext _dataContext;

        public BookListRepository(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<BookList>> GetAllBookListsAsync()
        {
            try
            {
                return await _dataContext.bookLists
                    .Include(bl => bl.User)
                    .Include(bl => bl.ListLikes)
                    .Include(bl => bl.BooksOfLists)
                        .ThenInclude(bol => bol.Book)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookList?> GetBookListByIdAsync(int id)
        {
            try
            {
                return await _dataContext.bookLists
                    .Include(bl => bl.User)
                    .Include(bl => bl.ListLikes)
                    .Include(bl => bl.BooksOfLists)
                        .ThenInclude(bol => bol.Book)
                    .FirstOrDefaultAsync(bl => bl.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookList>> GetBookListsByUserIdAsync(string userId)
        {
            try
            {
                return await _dataContext.bookLists
                    .Include(bl => bl.ListLikes)
                    .Include(bl => bl.BooksOfLists)
                        .ThenInclude(bol => bol.Book)
                    .Where(bl => bl.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookList> AddBookListAsync(BookList bookList)
        {
            try
            {
                await _dataContext.bookLists.AddAsync(bookList);
                await _dataContext.SaveChangesAsync();
                return bookList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookList?> UpdateBookListAsync(BookList bookList)
        {
            try
            {
                _dataContext.bookLists.Update(bookList);
                await _dataContext.SaveChangesAsync();
                return bookList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookList?> DeleteBookListAsync(BookList bookList)
        {
            try
            {
                _dataContext.bookLists.Remove(bookList);
                await _dataContext.SaveChangesAsync();
                return bookList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> BookListExistsAsync(int id)
        {
            try
            {
                return await _dataContext.bookLists.AnyAsync(bl => bl.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookList>> GetPopularBookListsAsync(int count)
        {
            try
            {
                return await _dataContext.bookLists
                    .Include(bl => bl.User)
                    .Include(bl => bl.ListLikes)
                    .Include(bl => bl.BooksOfLists)
                        .ThenInclude(bol => bol.Book)
                    .OrderByDescending(bl => bl.ListLikes != null ? bl.ListLikes.Count : 0)
                    .Take(count)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BooksOfList> AddBookToListAsync(BooksOfList bookOfList)
        {
            try
            {
                // Aynı kitabın listede bulunup bulunmadığını kontrol et
                var exists = await _dataContext.booksOfLists
                    .AnyAsync(bol => bol.ListId == bookOfList.ListId && bol.BookId == bookOfList.BookId);
                    
                if (exists)
                {
                    throw new InvalidOperationException("Bu kitap zaten listede mevcut.");
                }
                
                await _dataContext.booksOfLists.AddAsync(bookOfList);
                await _dataContext.SaveChangesAsync();
                return bookOfList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BooksOfList?> RemoveBookFromListAsync(int bookListId, int bookId)
        {
            try
            {
                var bookOfList = await _dataContext.booksOfLists
                    .FirstOrDefaultAsync(bol => bol.ListId == bookListId && bol.BookId == bookId);
                    
                if (bookOfList == null)
                {
                    return null;
                }
                
                _dataContext.booksOfLists.Remove(bookOfList);
                await _dataContext.SaveChangesAsync();
                return bookOfList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Book>> GetBooksInListAsync(int bookListId)
        {
            try
            {
                var books = await _dataContext.booksOfLists
                    .Where(bol => bol.ListId == bookListId)
                    .Include(bol => bol.Book)
                        .ThenInclude(b => b.Author)
                    .Select(bol => bol.Book)
                    .ToListAsync();
                    
                return books;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}