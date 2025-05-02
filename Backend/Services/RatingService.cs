using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Dtos.RatingDtos;
using Backend.Interface;
using Backend.Models;

namespace Backend.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository, 
                           IBookRepository bookRepository,
                           IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<List<RatingDto>> GetAllRatingsAsync()
        {
            try
            {
                var ratings = await _ratingRepository.GetAllRatingsAsync();
                return _mapper.Map<List<RatingDto>>(ratings);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RatingDto?> GetRatingByIdAsync(int id)
        {
            try
            {
                var rating = await _ratingRepository.GetRatingByIdAsync(id);
                if (rating == null)
                    return null;
                    
                return _mapper.Map<RatingDto>(rating);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RatingDto>> GetRatingsByBookAsync(int bookId)
        {
            try
            {
                var ratings = await _ratingRepository.GetRatingsByBookAsync(bookId);
                return _mapper.Map<List<RatingDto>>(ratings);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RatingDto>> GetRatingsByUserAsync(string userId)
        {
            try
            {
                var ratings = await _ratingRepository.GetRatingsByUserAsync(userId);
                return _mapper.Map<List<RatingDto>>(ratings);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RatingDto?> GetRatingByUserAndBookAsync(string userId, int bookId)
        {
            try
            {
                var rating = await _ratingRepository.GetRatingByUserAndBookAsync(userId, bookId);
                if (rating == null)
                    return null;
                    
                return _mapper.Map<RatingDto>(rating);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RatingDto> AddRatingAsync(CreateRatingDto ratingDto, string userId)
        {
            try
            {
                // Kullanıcının zaten değerlendirme yapıp yapmadığını kontrol et
                var existingRating = await _ratingRepository.GetRatingByUserAndBookAsync(userId, ratingDto.BookId);
                if (existingRating != null)
                {
                    throw new InvalidOperationException("Bu kitap için zaten bir değerlendirme yaptınız. Mevcut değerlendirmenizi güncelleyebilirsiniz.");
                }
                
                // Yeni rating oluştur
                var rating = _mapper.Map<Rating>(ratingDto);
                rating.UserId = userId;
                
                var createdRating = await _ratingRepository.AddRatingAsync(rating);
                
                // Kitabın ortalama puanını güncelle
                await UpdateBookAverageRatingAsync(ratingDto.BookId);
                
                return _mapper.Map<RatingDto>(createdRating);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RatingDto?> UpdateRatingAsync(int id, UpdateRatingDto ratingDto)
        {
            try
            {
                // Mevcut değerlendirmeyi bul
                var existingRating = await _ratingRepository.GetRatingByIdAsync(id);
                if (existingRating == null)
                    return null;
                    
                // Değerlendirmeyi güncelle
                _mapper.Map(ratingDto, existingRating);
                
                var updatedRating = await _ratingRepository.UpdateRatingAsync(existingRating);
                
                // Kitabın ortalama puanını güncelle
                if (updatedRating?.BookId != null)
                {
                    await UpdateBookAverageRatingAsync(updatedRating.BookId.Value);
                }
                
                return _mapper.Map<RatingDto>(updatedRating);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RatingDto?> DeleteRatingAsync(int id)
        {
            try
            {
                var rating = await _ratingRepository.GetRatingByIdAsync(id);
                if (rating == null)
                    return null;
                    
                var bookId = rating.BookId;
                
                var deletedRating = await _ratingRepository.DeleteRatingAsync(rating);
                
                // Kitabın ortalama puanını güncelle
                if (bookId != null)
                {
                    await UpdateBookAverageRatingAsync(bookId.Value);
                }
                
                return _mapper.Map<RatingDto>(deletedRating);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public async Task UpdateBookAverageRatingAsync(int bookId)
        {
            try
            {
                
                // Kitabı güncelle
                var book = await _bookRepository.GetBookByIdAsync(bookId);
                if (book != null)
                {
  
                    await _bookRepository.UpdateBookAsync(book);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}