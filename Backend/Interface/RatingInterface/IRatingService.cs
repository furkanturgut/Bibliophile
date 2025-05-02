using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Dtos.RatingDtos;

namespace Backend.Interface
{
    public interface IRatingService
    {
        /// <summary>
        /// Tüm değerlendirmeleri getirir
        /// </summary>
        Task<List<RatingDto>> GetAllRatingsAsync();
        
        /// <summary>
        /// ID'ye göre değerlendirme getirir
        /// </summary>
        Task<RatingDto?> GetRatingByIdAsync(int id);
        
        /// <summary>
        /// Kitaba göre değerlendirmeleri getirir
        /// </summary>
        Task<List<RatingDto>> GetRatingsByBookAsync(int bookId);
        
        /// <summary>
        /// Kullanıcıya göre değerlendirmeleri getirir
        /// </summary>
        Task<List<RatingDto>> GetRatingsByUserAsync(string userId);
        
        /// <summary>
        /// Kullanıcının belirli bir kitaba yaptığı değerlendirmeyi getirir
        /// </summary>
        Task<RatingDto?> GetRatingByUserAndBookAsync(string userId, int bookId);
        
        /// <summary>
        /// Yeni bir değerlendirme ekler
        /// </summary>
        Task<RatingDto> AddRatingAsync(CreateRatingDto ratingDto, string userId);
        
        /// <summary>
        /// Var olan bir değerlendirmeyi günceller
        /// </summary>
        Task<RatingDto?> UpdateRatingAsync(int id, UpdateRatingDto ratingDto);
        
        /// <summary>
        /// Bir değerlendirmeyi siler
        /// </summary>
        Task<RatingDto?> DeleteRatingAsync(int id);
        
        
        /// <summary>
        /// Kitabın ortalama puanını günceller
        /// </summary>
        Task UpdateBookAverageRatingAsync(int bookId);
    }
}