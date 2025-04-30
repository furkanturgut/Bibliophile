using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.GenreDtos;

namespace Backend.Interface.GenreInterface
{
    public interface IGenreService
    {
        public Task<bool> GenreExist (int genreId);
        public Task<List<GenreDto>> GetAllGenres ();
        public Task<GenreDto> GetGenreById (int genreId);
        public Task<List<GenreDto>> GetGenreByName (string name);
    }
}