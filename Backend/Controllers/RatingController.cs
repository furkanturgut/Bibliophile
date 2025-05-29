using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Dtos.RatingDtos;
using Backend.Extensions;
using Backend.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<RatingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRatings()
        {
            try
            {
                var ratings = await _ratingService.GetAllRatingsAsync();
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{ratingId:int}")]
        [ProducesResponseType(typeof(RatingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRatingById(int ratingId)
        {
            try
            {
                var rating = await _ratingService.GetRatingByIdAsync(ratingId);
                return Ok(rating);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"ID {ratingId} ile değerlendirme bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("book/{bookId:int}")]
        [ProducesResponseType(typeof(List<RatingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRatingsByBook(int bookId)
        {
            try
            {
                var ratings = await _ratingService.GetRatingsByBookAsync(bookId);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(List<RatingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       
        public async Task<IActionResult> GetRatingsByUser(string userId)
        {
            try
            {
                var ratings = await _ratingService.GetRatingsByUserAsync(userId);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


        [HttpGet("my-ratings")]
        [ProducesResponseType(typeof(List<RatingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> GetMyRatings()
        {
            try
            {
                var userName = User.GetUserName();
                var ratings = await _ratingService.GetRatingsByUserAsync(userName);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("my-rating/book/{bookId:int}")]
        [ProducesResponseType(typeof(RatingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> GetMyRatingForBook(int bookId)
        {
            try
            {
                var userName = User.GetUserName();
                var rating = await _ratingService.GetRatingByUserAndBookAsync(userName, bookId);
                return Ok(rating);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Bu kitap için değerlendirmeniz bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(RatingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> AddRating([FromBody] CreateRatingDto ratingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userName = User.GetUserName();
                var createdRating = await _ratingService.AddRatingAsync(ratingDto, userName);

                return Ok(createdRating);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }


        [HttpPut("{ratingId:int}")]
        [ProducesResponseType(typeof(RatingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRating(int ratingId, [FromBody] UpdateRatingDto ratingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Değerlendirmenin sahibi olup olmadığını kontrol et
                var rating = await _ratingService.GetRatingByIdAsync(ratingId);

                var userName = User.GetUserName(); 
                if (rating.UserName != userName && !User.IsInRole("Admin"))
                    return Forbid("Bu değerlendirmeyi düzenleme yetkiniz yok");

                var updatedRating = await _ratingService.UpdateRatingAsync(ratingId, ratingDto);
                return Ok(updatedRating);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"ID {ratingId} ile değerlendirme bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{ratingId:int}")]
        [ProducesResponseType(typeof(RatingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]

        public async Task<IActionResult> DeleteRating(int ratingId)
        {
            try
            {
                // Değerlendirmenin sahibi olup olmadığını kontrol et
                var rating = await _ratingService.GetRatingByIdAsync(ratingId);

                var userName = User.GetUserName();
                var deletedRating = await _ratingService.DeleteRatingAsync(ratingId);
                return Ok(deletedRating);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"ID {ratingId} ile değerlendirme bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("average/{bookId:int}")]

        public async Task<IActionResult> CalculateAverageRating(int bookId)
        {
            try
            {
                var AverageRating = await _ratingService.CalculateAverageRatingAsync(bookId);
                return Ok(AverageRating);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}