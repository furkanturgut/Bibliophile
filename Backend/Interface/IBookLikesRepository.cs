using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IBookLikesRepository
    {
        /// <summary>
        /// Tüm kitap beğenilerini getirir
        /// </summary>
        Task<List<BookLikes>> GetAllBookLikesAsync();
        
        /// <summary>
        /// Bir kitabın tüm beğenilerini getirir
        /// </summary>
        Task<List<BookLikes>> GetLikesByBookIdAsync(int bookId);
        
        /// <summary>
        /// Bir kullanıcının beğendiği tüm kitapları getirir
        /// </summary>
        Task<List<BookLikes>> GetBookLikesByUserIdAsync(string userId);
        
        /// <summary>
        /// Kullanıcının bir kitabı beğenip beğenmediğini kontrol eder
        /// </summary>
        Task<BookLikes> GetBookLikeByUserAndBookAsync(string userId, int bookId);
        
        /// <summary>
        /// Yeni bir beğeni ekler
        /// </summary>
        Task<BookLikes> AddBookLikeAsync(BookLikes bookLike);
        
        /// <summary>
        /// Bir beğeniyi kaldırır
        /// </summary>
        Task<BookLikes> RemoveBookLikeAsync(BookLikes bookLike);
        
        /// <summary>
        /// Bir kitabın beğeni sayısını getirir
        /// </summary>
        Task<int> GetBookLikesCountAsync(int bookId);
        
        /// <summary>
        /// Kullanıcının bir kitabı beğenip beğenmediğini kontrol eder
        /// </summary>
        Task<bool> IsBookLikedByUserAsync(string userId, int bookId);
    }
}