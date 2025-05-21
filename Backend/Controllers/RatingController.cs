using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Dtos.RatingDtos;
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
        public async Task<IActionResult> GetMyRatings()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ratings = await _ratingService.GetRatingsByUserAsync(userId);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


        [HttpGet("my-rating/book/{bookId:int}")]
        [ProducesResponseType(typeof(RatingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMyRatingForBook(int bookId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var rating = await _ratingService.GetRatingByUserAndBookAsync(userId, bookId);
                return Ok(rating);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Bu kitap için değerlendirmeniz bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(RatingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRating([FromBody] CreateRatingDto ratingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var createdRating = await _ratingService.AddRatingAsync(ratingDto, userId);

                return CreatedAtAction(nameof(GetRatingById), new { id = createdRating.Id }, createdRating);
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

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (rating.UserId != userId && !User.IsInRole("Admin"))
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

        public async Task<IActionResult> DeleteRating(int ratingId)
        {
            try
            {
                // Değerlendirmenin sahibi olup olmadığını kontrol et
                var rating = await _ratingService.GetRatingByIdAsync(ratingId);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (rating.UserId != userId && !User.IsInRole("Admin"))
                    return Forbid("Bu değerlendirmeyi silme yetkiniz yok");

                var deletedRating = await _ratingService.DeleteRatingAsync(ratingId);
                return Ok(deletedRating);
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


        [HttpGet("average/{bookId:int}")]

        public async Task<IActionResult> CalculateAverageRating(int bookId)
        {
            try
            {
        
                return Ok(new { BookId = bookId });
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}