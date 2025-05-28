using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public BookLikesService(IBookLikesRepository bookLikesRepository, IBookRepository bookRepository, UserManager<AppUser> userManager)
        {
            _bookLikesRepository = bookLikesRepository;
            _bookRepository = bookRepository;
            this._userManager = userManager;
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

        public async Task<bool> IsBookLikedByUserAsync(string userName, int bookId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                return await _bookLikesRepository.IsBookLikedByUserAsync(user.Id, bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ToggleBookLikeAsync(string userName, int bookId)
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
                var user = await _userManager.FindByNameAsync(userName);
                var existingLike = await _bookLikesRepository.GetBookLikeByUserAndBookAsync(user.Id, bookId);
                
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
                        UserId = user.Id
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

        public async Task<List<int?>> GetUserLikedBookIdsAsync(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var userLikes = await _bookLikesRepository.GetBookLikesByUserIdAsync(user.Id);
                return  userLikes.Select(bl => bl.BookId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}