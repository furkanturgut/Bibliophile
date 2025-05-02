using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IRatingRepository
    {
        /// <summary>
        /// Tüm değerlendirmeleri getirir
        /// </summary>
        Task<List<Rating>> GetAllRatingsAsync();
        
        /// <summary>
        /// ID'ye göre değerlendirme getirir
        /// </summary>
        Task<Rating?> GetRatingByIdAsync(int id);
        
        /// <summary>
        /// Kitaba göre değerlendirmeleri getirir
        /// </summary>
        Task<List<Rating>> GetRatingsByBookAsync(int bookId);
        
        /// <summary>
        /// Kullanıcıya göre değerlendirmeleri getirir
        /// </summary>
        Task<List<Rating>> GetRatingsByUserAsync(string userId);
        
        /// <summary>
        /// Kullanıcının belirli bir kitaba yaptığı değerlendirmeyi getirir
        /// </summary>
        Task<Rating?> GetRatingByUserAndBookAsync(string userId, int bookId);
        
        /// <summary>
        /// Yeni bir değerlendirme ekler
        /// </summary>
        Task<Rating> AddRatingAsync(Rating rating);
        
        /// <summary>
        /// Var olan bir değerlendirmeyi günceller
        /// </summary>
        Task<Rating?> UpdateRatingAsync(Rating rating);
        
        /// <summary>
        /// Bir değerlendirmeyi siler
        /// </summary>
        Task<Rating?> DeleteRatingAsync(Rating rating);
    }
}