using Backend.Data;
using Backend.Interface;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    public class BookLikesRepository : IBookLikesRepository
    {
        private readonly ApplicationDataContext _dataContext;

        public BookLikesRepository(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<BookLikes>> GetAllBookLikesAsync()
        {
            try
            {
                return await _dataContext.bookLikes
                    .Include(bl => bl.User)
                    .Include(bl => bl.Book)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookLikes>> GetLikesByBookIdAsync(int bookId)
        {
            try
            {
                return await _dataContext.bookLikes
                    .Include(bl => bl.User)
                    .Where(bl => bl.BookId == bookId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookLikes>> GetBookLikesByUserIdAsync(string userId)
        {
            try
            {
                return await _dataContext.bookLikes
                    .Include(bl => bl.Book)
                    .Where(bl => bl.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookLikes> GetBookLikeByUserAndBookAsync(string userId, int bookId)
        {
            try
            {
                return await _dataContext.bookLikes
                    .FirstOrDefaultAsync(bl => bl.UserId == userId && bl.BookId == bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookLikes> AddBookLikeAsync(BookLikes bookLike)
        {
            try
            {
                await _dataContext.bookLikes.AddAsync(bookLike);
                await _dataContext.SaveChangesAsync();
                return bookLike;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookLikes> RemoveBookLikeAsync(BookLikes bookLike)
        {
            try
            {
                _dataContext.bookLikes.Remove(bookLike);
                await _dataContext.SaveChangesAsync();
                return bookLike;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetBookLikesCountAsync(int bookId)
        {
            try
            {
                return await _dataContext.bookLikes
                    .Where(bl => bl.BookId == bookId)
                    .CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsBookLikedByUserAsync(string userId, int bookId)
        {
            try
            {
                return await _dataContext.bookLikes
                    .AnyAsync(bl => bl.UserId == userId && bl.BookId == bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}