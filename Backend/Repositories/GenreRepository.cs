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
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDataContext _dataContext;

        public GenreRepository(ApplicationDataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<bool> GenreExist(int genreId)
        {
            return await _dataContext.genres.AnyAsync(g=> g.Id==genreId);
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return await _dataContext.genres.ToListAsync();
        }

        public async Task<Genre?> GetGenreById(int genreId)
        {
            return await _dataContext.genres.FindAsync(genreId);
            
        }

        public async Task<List<Genre>?> GetGenreByName(string name)
        {
            return await _dataContext.genres
                .Where(g => g.Name.Contains(name))
                .ToListAsync();
        }
    }
}