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
    [Route("api/lists/{listId}/likes")]
    public class ListLikeController : ControllerBase
    {
        private readonly IListLikeService _listLikeService;

        public ListLikeController(IListLikeService listLikeService)
        {
            _listLikeService = listLikeService;
        }


        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListLikesCount(int listId)
        {
            try
            {
                var likesCount = await _listLikeService.GetListLikesCountAsync(listId);
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
        public async Task<IActionResult> IsListLikedByUser(int listId)
        {
            try
            {
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isLiked = await _listLikeService.IsListLikedByUserAsync(userId, listId);
                return Ok(new { IsLiked = isLiked });
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


        [HttpPost("toggle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ToggleListLike(int listId)
        {
            try
            {
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isLiked = await _listLikeService.ToggleListLikeAsync(userId, listId);
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