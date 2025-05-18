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
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDataContext _dataContext;

        public RatingRepository(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Rating>> GetAllRatingsAsync()
        {
            try
            {
                return await _dataContext.ratings
                    .Include(r => r.User)
                    .Include(r => r.Book)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Rating?> GetRatingByIdAsync(int id)
        {
            try
            {
                return await _dataContext.ratings
                    .Include(r => r.User)
                    .Include(r => r.Book)
                    .FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Rating>> GetRatingsByBookAsync(int bookId)
        {
            try
            {
                return await _dataContext.ratings
                    .Where(r => r.BookId == bookId)
                    .Include(r => r.User)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Rating>> GetRatingsByUserAsync(string userId)
        {
            try
            {
                return await _dataContext.ratings
                    .Where(r => r.UserId == userId)
                    .Include(r => r.Book)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Rating?> GetRatingByUserAndBookAsync(string userId, int bookId)
        {
            try
            {
                return await _dataContext.ratings
                    .FirstOrDefaultAsync(r => r.UserId == userId && r.BookId == bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Rating> AddRatingAsync(Rating rating)
        {
            try
            {
                // Şu anki zamanı ata
                rating.CreatedAt = DateTime.Now;
                rating.UpdatedAt = DateTime.Now;
                
                await _dataContext.ratings.AddAsync(rating);
                await _dataContext.SaveChangesAsync();
                

                
                return rating;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Rating?> UpdateRatingAsync(Rating rating)
        {
            try
            {
                // Güncelleme zamanını ata
                rating.UpdatedAt = DateTime.Now;
                
                _dataContext.ratings.Update(rating);
                await _dataContext.SaveChangesAsync();
                
                
                return rating;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Rating?> DeleteRatingAsync(Rating rating)
        {
            try
            {  
                _dataContext.ratings.Remove(rating);
                await _dataContext.SaveChangesAsync();             
                return rating;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<double> CalculateAverageRatingForBookAsync(int bookId)
        {
            try
            {
                var ratings = await _dataContext.ratings
                    .Where(r => r.BookId == bookId)
                    .Select(r => r.Rate)
                    .ToListAsync();
                
                if (ratings == null || !ratings.Any())
                    return 0;
                    
                return Math.Round(ratings.Average(), 2);
            }
            catch (Exception)
            {
                throw;
            }
        }
        }
    }
