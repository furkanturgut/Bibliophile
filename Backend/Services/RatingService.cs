using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Dtos.RatingDtos;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public RatingService(IRatingRepository ratingRepository, IBookRepository bookRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _ratingRepository = ratingRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            this._userManager = userManager;
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
                var rating = await _ratingRepository.GetRatingByIdAsync(id) ?? throw new KeyNotFoundException();
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

        public async Task<List<RatingDto>> GetRatingsByUserAsync(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var ratings = await _ratingRepository.GetRatingsByUserAsync(user.Id);
                return _mapper.Map<List<RatingDto>>(ratings);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RatingDto?> GetRatingByUserAndBookAsync(string userName, int bookId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var rating = await _ratingRepository.GetRatingByUserAndBookAsync(user.Id, bookId);
                if (rating == null)
                    throw new KeyNotFoundException();

                return _mapper.Map<RatingDto>(rating);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RatingDto> AddRatingAsync(CreateRatingDto ratingDto, string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                // Kullanıcının zaten değerlendirme yapıp yapmadığını kontrol et
                var existingRating = await _ratingRepository.GetRatingByUserAndBookAsync(user.Id, ratingDto.BookId);
                if (existingRating != null)
                {
                    throw new InvalidOperationException("Bu kitap için zaten bir değerlendirme yaptınız. Mevcut değerlendirmenizi güncelleyebilirsiniz.");
                }

                // Yeni rating oluştur
                var rating = _mapper.Map<Rating>(ratingDto);
                rating.UserId = user.Id;

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
                var rating = await _ratingRepository.GetRatingByIdAsync(id) ?? throw new KeyNotFoundException();


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
                var averageRating = await CalculateAverageRatingAsync(bookId);
                var book = await _bookRepository.GetBookByIdAsync(bookId);
                
                if (book != null)
                {
                    book.AvarageRating = (decimal)averageRating;
                    await _bookRepository.UpdateBookAsync(book);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<double> CalculateAverageRatingAsync(int bookId)
        {
            try
            {
                var ratings = await _ratingRepository.GetRatingsByBookAsync(bookId);
                
                if (ratings == null || ratings.Count == 0)
                    return 0.0;
                
                var sum = ratings.Sum(r => r.Rate);
                return Math.Round((double)sum / ratings.Count, 2);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}