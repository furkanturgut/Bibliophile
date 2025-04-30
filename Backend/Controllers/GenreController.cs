using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Dtos.GenreDtos;
using Backend.Interface.GenreInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Get all available genres
        /// </summary>
        /// <returns>List of all genres</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            try
            {
                var genres = await _genreService.GetAllGenres();
                return Ok(genres);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error while retrieving genres");
            }
        }

        /// <summary>
        /// Get a genre by its ID
        /// </summary>
        /// <returns>The genre with the specified ID</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenreById(int id)
        {
            try
            {
                var genre = await _genreService.GetGenreById(id);
                return Ok(genre);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Genre with ID {id} not found");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error while retrieving the genre");
            }
        }

        /// <summary>
        /// Search for genres by name
        /// </summary>
        /// <returns>Genres matching the search term</returns>
        [HttpGet("search")]
        public async Task<IActionResult> GetGenresByName([FromQuery] string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest("Search term cannot be empty");
                }

                var genres = await _genreService.GetGenreByName(name);
                return Ok(genres);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"No genres found matching '{name}'");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error while searching for genres");
            }
        }

        /// <summary>
        /// Check if a genre exists
        /// </summary>
        /// <returns>True if the genre exists, false otherwise</returns>
        [HttpGet("exists/{id:int}")]
        public async Task<IActionResult> GenreExists(int id)
        {
            try
            {
                var exists = await _genreService.GenreExist(id);
                return Ok(exists);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error while checking genre existence");
            }
        }
    }
}