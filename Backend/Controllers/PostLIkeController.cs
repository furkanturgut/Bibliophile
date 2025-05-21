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


        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPostLikesCount(int postId)
        {
            try
            {
                var likesCount = await _postLikeService.GetPostLikesCountAsync(postId);
                return Ok(new { Count = likesCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("is-liked")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IsPostLikedByUser(int postId)
        {
            try
            {
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isLiked = await _postLikeService.IsPostLikedByUserAsync(userId, postId);
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
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}