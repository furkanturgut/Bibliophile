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


        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookLikesCount(int bookId)
        {
            try
            {
                var likesCount = await _bookLikesService.GetBookLikesCountAsync(bookId);
                return Ok(new { Count = likesCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


        [HttpGet("is-liked")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> IsBookLikedByUser(int bookId)
        {
            try
            {
                var userId ="02017753-34b3-4c58-a873-98b46937fef2";
                var isLiked = await _bookLikesService.IsBookLikedByUserAsync(userId, bookId);
                return Ok(new { IsLiked = isLiked });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("toggle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [HttpGet("/api/users/me/liked-books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserLikedBooks()
        {
            try
            {
                var userId ="02017753-34b3-4c58-a873-98b46937fef2";
                var likedBookIds = await _bookLikesService.GetUserLikedBookIdsAsync(userId);
                return Ok(new { BookIds = likedBookIds });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}