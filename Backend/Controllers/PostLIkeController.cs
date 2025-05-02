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
    [Route("api/posts/{postId}/likes")]
    public class PostLikeController : ControllerBase
    {
        private readonly IPostLikeService _postLikeService;

        public PostLikeController(IPostLikeService postLikeService)
        {
            _postLikeService = postLikeService;
        }

        /// <summary>
        /// Bir blog yazısının beğeni sayısını getirir
        /// </summary>
        /// <returns>Beğeni sayısı</returns>
        [HttpGet("count")]
        public async Task<IActionResult> GetPostLikesCount(int postId)
        {
            try
            {
                var likesCount = await _postLikeService.GetPostLikesCountAsync(postId);
                return Ok(new { Count = likesCount });
            }
            catch (Exception)
            {
                return StatusCode(500, "Beğeni sayısı getirilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Giriş yapmış kullanıcının, blog yazısını beğenip beğenmediğini kontrol eder
        /// </summary>
        /// <returns>Beğeni durumu</returns>
        [HttpGet("is-liked")]
        public async Task<IActionResult> IsPostLikedByUser(int postId)
        {
            try
            {
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isLiked = await _postLikeService.IsPostLikedByUserAsync(userId, postId);
                return Ok(new { IsLiked = isLiked });
            }
            catch (Exception)
            {
                return StatusCode(500, "Beğeni durumu kontrol edilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Bir blog yazısını beğenir veya beğeniyi kaldırır
        /// </summary>
        /// <returns>Güncel beğeni durumu</returns>
        [HttpPost("toggle")]
        public async Task<IActionResult> TogglePostLike(int postId)
        {
            try
            {
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isLiked = await _postLikeService.TogglePostLikeAsync(userId, postId);
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
    }
}