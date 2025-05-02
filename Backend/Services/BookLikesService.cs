using Backend.Interface;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class BookLikesService : IBookLikesService
    {
        private readonly IBookLikesRepository _bookLikesRepository;
        private readonly IBookRepository _bookRepository;

        public BookLikesService(IBookLikesRepository bookLikesRepository, IBookRepository bookRepository)
        {
            _bookLikesRepository = bookLikesRepository;
            _bookRepository = bookRepository;
        }

        public async Task<int> GetBookLikesCountAsync(int bookId)
        {
            try
            {
                return await _bookLikesRepository.GetBookLikesCountAsync(bookId);
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
                return await _bookLikesRepository.IsBookLikedByUserAsync(userId, bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ToggleBookLikeAsync(string userId, int bookId)
        {
            try
            {
                // Kitabın var olup olmadığını kontrol et
                var book = await _bookRepository.GetBookByIdAsync(bookId);
                if (book == null)
                {
                    throw new KeyNotFoundException($"ID {bookId} ile kitap bulunamadı");
                }

                // Kullanıcının kitabı beğenip beğenmediğini kontrol et
                var existingLike = await _bookLikesRepository.GetBookLikeByUserAndBookAsync(userId, bookId);
                
                if (existingLike != null)
                {
                    // Beğeni varsa kaldır
                    await _bookLikesRepository.RemoveBookLikeAsync(existingLike);
                    return false; // Artık beğenilmiyor
                }
                else
                {
                    // Beğeni yoksa ekle
                    var newLike = new BookLikes
                    {
                        BookId = bookId,
                        UserId = userId
                    };
                    
                    await _bookLikesRepository.AddBookLikeAsync(newLike);
                    return true; // Şimdi beğeniliyor
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<int?>> GetUserLikedBookIdsAsync(string userId)
        {
            try
            {
                var userLikes = await _bookLikesRepository.GetBookLikesByUserIdAsync(userId);
                return  userLikes.Select(bl => bl.BookId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}