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

        /// <summary>
        /// Tüm değerlendirmeleri getirir
        /// </summary>
        /// <returns>Değerlendirmelerin listesi</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRatings()
        {
            try
            {
                var ratings = await _ratingService.GetAllRatingsAsync();
                return Ok(ratings);
            }
            catch (Exception)
            {
                return StatusCode(500, "Değerlendirmeleri getirirken bir hata oluştu");
            }
        }

        /// <summary>
        /// ID'ye göre değerlendirme getirir
        /// </summary>
        /// <returns>ID'ye göre değerlendirme</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRatingById(int id)
        {
            try
            {
                var rating = await _ratingService.GetRatingByIdAsync(id);
                if (rating == null)
                    return NotFound($"ID {id} ile değerlendirme bulunamadı");
                
                return Ok(rating);
            }
            catch (Exception)
            {
                return StatusCode(500, "Değerlendirme getirirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Kitaba göre değerlendirmeleri getirir
        /// </summary>     
        /// <returns>Kitaba ait değerlendirmeler</returns>
        [HttpGet("book/{bookId:int}")]
        public async Task<IActionResult> GetRatingsByBook(int bookId)
        {
            try
            {
                var ratings = await _ratingService.GetRatingsByBookAsync(bookId);
                return Ok(ratings);
            }
            catch (Exception)
            {
                return StatusCode(500, "Kitap değerlendirmelerini getirirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Kullanıcıya göre değerlendirmeleri getirir
        /// </summary>

        /// <returns>Kullanıcıya ait değerlendirmeler</returns>
        [HttpGet("user/{userId}")]
       
        public async Task<IActionResult> GetRatingsByUser(string userId)
        {
            try
            {
                var ratings = await _ratingService.GetRatingsByUserAsync(userId);
                return Ok(ratings);
            }
            catch (Exception)
            {
                return StatusCode(500, "Kullanıcı değerlendirmelerini getirirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Kullanıcının kendi değerlendirmelerini getirir
        /// </summary>
        /// <returns>Giriş yapmış kullanıcıya ait değerlendirmeler</returns>
        [HttpGet("my-ratings")]

        public async Task<IActionResult> GetMyRatings()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ratings = await _ratingService.GetRatingsByUserAsync(userId);
                return Ok(ratings);
            }
            catch (Exception)
            {
                return StatusCode(500, "Değerlendirmelerinizi getirirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Kullanıcının belirli bir kitap için değerlendirmesini getirir
        /// </summary>
        /// <returns>Kullanıcının kitap değerlendirmesi</returns>
        [HttpGet("my-rating/book/{bookId:int}")]
        [Authorize]

        public async Task<IActionResult> GetMyRatingForBook(int bookId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var rating = await _ratingService.GetRatingByUserAndBookAsync(userId, bookId);
                
                if (rating == null)
                    return NotFound($"Bu kitap için değerlendirmeniz bulunamadı");
                
                return Ok(rating);
            }
            catch (Exception)
            {
                return StatusCode(500, "Değerlendirme getirirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Yeni bir değerlendirme ekler
        /// </summary>
        /// <returns>Oluşturulan değerlendirme</returns>
        [HttpPost]
     
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

        /// <summary>
        /// Var olan bir değerlendirmeyi günceller
        /// </summary>

        /// <returns>Güncellenen değerlendirme</returns>
        [HttpPut("{id:int}")]

        public async Task<IActionResult> UpdateRating(int id, [FromBody] UpdateRatingDto ratingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Değerlendirmenin sahibi olup olmadığını kontrol et
                var rating = await _ratingService.GetRatingByIdAsync(id);
                if (rating == null)
                    return NotFound($"ID {id} ile değerlendirme bulunamadı");
                
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (rating.UserId != userId && !User.IsInRole("Admin"))
                    return Forbid("Bu değerlendirmeyi düzenleme yetkiniz yok");

                var updatedRating = await _ratingService.UpdateRatingAsync(id, ratingDto);
                return Ok(updatedRating);
            }
            catch (Exception)
            {
                return StatusCode(500, "Değerlendirme güncellenirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Bir değerlendirmeyi siler
        /// </summary>

        /// <returns>Silinen değerlendirme</returns>
        [HttpDelete("{id:int}")]


        public async Task<IActionResult> DeleteRating(int id)
        {
            try
            {
                // Değerlendirmenin sahibi olup olmadığını kontrol et
                var rating = await _ratingService.GetRatingByIdAsync(id);
                if (rating == null)
                    return NotFound($"ID {id} ile değerlendirme bulunamadı");
                
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (rating.UserId != userId && !User.IsInRole("Admin"))
                    return Forbid("Bu değerlendirmeyi silme yetkiniz yok");

                var deletedRating = await _ratingService.DeleteRatingAsync(id);
                return Ok(deletedRating);
            }
            catch (Exception)
            {
                return StatusCode(500, "Değerlendirme silinirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Bir kitabın ortalama puanını hesaplar
        /// </summary>
        /// <returns>Kitabın ortalama puanı</returns>
        [HttpGet("average/{bookId:int}")]

        public async Task<IActionResult> CalculateAverageRating(int bookId)
        {
            try
            {
        
                return Ok(new { BookId = bookId });
            }
            catch (Exception)
            {
                return StatusCode(500, "Ortalama puan hesaplanırken bir hata oluştu");
            }
        }
    }
}