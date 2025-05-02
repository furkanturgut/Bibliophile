using Backend.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/books/{bookId}/likes")]
    public class BookLikesController : ControllerBase
    {
        private readonly IBookLikesService _bookLikesService;

        public BookLikesController(IBookLikesService bookLikesService)
        {
            _bookLikesService = bookLikesService;
        }

        /// <summary>
        /// Bir kitabın beğeni sayısını getirir
        /// </summary>
        /// <returns>Beğeni sayısı</returns>
        [HttpGet("count")]
        public async Task<IActionResult> GetBookLikesCount(int bookId)
        {
            try
            {
                var likesCount = await _bookLikesService.GetBookLikesCountAsync(bookId);
                return Ok(new { Count = likesCount });
            }
            catch (Exception)
            {
                return StatusCode(500, "Beğeni sayısı getirilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Giriş yapmış kullanıcının, kitabı beğenip beğenmediğini kontrol eder
        /// </summary>
        /// <returns>Beğeni durumu</returns>
        [HttpGet("is-liked")]

        public async Task<IActionResult> IsBookLikedByUser(int bookId)
        {
            try
            {
                var userId ="02017753-34b3-4c58-a873-98b46937fef2";
                var isLiked = await _bookLikesService.IsBookLikedByUserAsync(userId, bookId);
                return Ok(new { IsLiked = isLiked });
            }
            catch (Exception)
            {
                return StatusCode(500, "Beğeni durumu kontrol edilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Bir kitabı beğenir veya beğeniyi kaldırır
        /// </summary>
        /// <returns>Güncel beğeni durumu</returns>
        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleBookLike(int bookId)
        {
            try
            {
                var userId ="02017753-34b3-4c58-a873-98b46937fef2";
                var isLiked = await _bookLikesService.ToggleBookLikeAsync(userId, bookId);
                return Ok(new { IsLiked = isLiked });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Beğeni işlemi sırasında bir hata oluştu");
            }
        }

        /// <summary>
        /// Giriş yapmış kullanıcının beğendiği tüm kitapların ID'lerini getirir
        /// </summary>
        /// <returns>Beğenilen kitapların ID listesi</returns>
        [HttpGet("/api/users/me/liked-books")]
        public async Task<IActionResult> GetUserLikedBooks()
        {
            try
            {
                var userId ="02017753-34b3-4c58-a873-98b46937fef2";
                var likedBookIds = await _bookLikesService.GetUserLikedBookIdsAsync(userId);
                return Ok(new { BookIds = likedBookIds });
            }
            catch (Exception)
            {
                return StatusCode(500, "Beğenilen kitaplar getirilirken bir hata oluştu");
            }
        }
    }
}