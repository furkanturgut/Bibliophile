using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IGenreRepository
    {
        Task<Genre?> GetGenreById (int genreId);
        Task<bool> GenreExist (int genreId);
        Task<List<Genre>?> GetGenreByName (string name);
        Task<List<Genre>> GetAllGenres ();
    }
}