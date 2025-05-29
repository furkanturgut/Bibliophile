using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Dtos.RatingDtos;

namespace Backend.Interface
{
    public interface IRatingService
    {
        Task<List<RatingDto>> GetAllRatingsAsync();
        Task<RatingDto?> GetRatingByIdAsync(int id);
        Task<List<RatingDto>> GetRatingsByBookAsync(int bookId);
        Task<List<RatingDto>> GetRatingsByUserAsync(string userName);
        Task<RatingDto?> GetRatingByUserAndBookAsync(string userName, int bookId);
        Task<RatingDto> AddRatingAsync(CreateRatingDto ratingDto, string userName);
        Task<RatingDto?> UpdateRatingAsync(int id, UpdateRatingDto ratingDto);
        Task<RatingDto?> DeleteRatingAsync(int id);
        Task UpdateBookAverageRatingAsync(int bookId);
        Task<double> CalculateAverageRatingAsync(int bookId);
    }   
}