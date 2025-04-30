using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Dtos.GenreDtos;
using Backend.Interface;
using Backend.Interface.GenreInterface;

namespace Backend.Services
{
    public class GenreService: IGenreService
    {
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;

        public GenreService(IMapper mapper, IGenreRepository genreRepository)
        {
            this._mapper = mapper;
            this._genreRepository = genreRepository;
        }

        public Task<bool> GenreExist(int genreId)
        {
            try
            {
                return _genreRepository.GenreExist(genreId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GenreDto>> GetAllGenres()
        {
            try
            {
                var genres = await _genreRepository.GetAllGenres();
                return _mapper.Map<List<GenreDto>>(genres);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GenreDto> GetGenreById(int genreId)
        {
            try
            {
                var genre = await _genreRepository.GetGenreById(genreId);
                if (genre is null)
                {
                    throw new KeyNotFoundException();
                }
                return _mapper.Map<GenreDto>(genre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GenreDto>> GetGenreByName(string name)
        {
            try
            {
                var genres = await _genreRepository.GetGenreByName(name);
                if (genres is null)
                {
                    throw new KeyNotFoundException();
                }
                return _mapper.Map<List<GenreDto>>(genres);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}